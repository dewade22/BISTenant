using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Manufacturing.Data;
using System.Data;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Manufacturing.Domain.Data;
using Manufacturing.Domain.Multitenancy;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Manufacturing.Controllers
{
    public class AccountController : Controller
    {
        private readonly SystemDbContext _context;
        private readonly ApplicationDbContext _ApplicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _config;
        

        public AccountController(
            SystemDbContext context,
            ApplicationDbContext applicationDbContext,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration config)
        {
            _context = context;
            _ApplicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }

        public string ReturnUrl { get; set; }

        public IActionResult Index(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //public IActionResult Validate(SystemUsers SystemUsers, string returnUrl = null)
        //dibawah untuk JWT
        //[AllowAnonymous]
        [HttpPost]
        public IActionResult Validate(SystemUsers SystemUsers, string returnUrl = null, string ClientOs = "", string ClientBrowser = "", int ClientMemory = 0, int ClientCore = 0)
        {
            IActionResult response = Unauthorized(); 
            
            var client = _httpContextAccessor.HttpContext.GetClient();
            if (client != null)
            {
                if (SystemUsers.EMailAddress != "" && SystemUsers.EMailAddress != null)
                {
                    var user = AuthUsers(SystemUsers);
                    SystemUsers _SystemsUsers = _context.SystemUsers.Where(s => s.EMailAddress == SystemUsers.EMailAddress).FirstOrDefault();
                    if (_SystemsUsers != null)
                    {
                        string userpass = dbSecurity.MD5(SystemUsers.UserPassword);
                        if (_SystemsUsers.UserPassword == userpass)
                        {
                            //JWT Create Token
                            
                            var tokenString = GenerateJSONWebToken(user);
                            
   
                            HttpContext.Session.SetString("EMailAddress", _SystemsUsers.EMailAddress);
                            HttpContext.Session.SetString("UserCode", _SystemsUsers.UserName);
                            SystemUserRoles _systemuserroles = _context.SystemUserRoles.FirstOrDefault(a => a.UserCode == _SystemsUsers.UserCode && a.DefaultCompany == 1);

                            //_CompanyInformationService.GetCompanyInformationByCode(_systemuserroles.CompanyCode);
                            //CompanyInformation _CompanyInformations = _context.CompanyInformation..FirstOrDefault(a => a.UserCode == _systemuserroles.CompanyCode);

                            HttpContext.Session.SetString("Role", _systemuserroles.RoleID);
                            HttpContext.Session.SetString("CompanyCode", _systemuserroles.CompanyCode);
                            string roles = HttpContext.Session.GetString("Role");
                            List<SystemUserMenu> SystemUserMenus = _context.SystemUserMenu.Where(s => s.RoleID == roles && s.CompanyCode == _systemuserroles.CompanyCode).OrderBy(a => a.SeqNo).ToList();
                            DataSet ds = new DataSet();
                            ds = ToDataSet(SystemUserMenus);
                            DataTable table = ds.Tables[0];
                            DataRow[] parentMenus = table.Select("ParentId = 1");
                            var sb = new StringBuilder();
                            string menuString = GenerateURL(parentMenus, table, sb);
                            HttpContext.Session.SetString("menuString", menuString);
                            HttpContext.Session.SetString("menus", JsonConvert.SerializeObject(SystemUserMenus));

                            //Get Tenant Active
                            string tenantActive = GetFullTenant();
                            HttpContext.Session.SetString("tenant", tenantActive);

                            /*//Save Login Information
                            var LogActivity = new WebLoginActivity();
                            LogActivity.Email = HttpContext.Session.GetString("EMailAddress");
                            LogActivity.IP_Addr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                            LogActivity.LastLoginTime = DateTime.Now;
                            LogActivity.ClientOS = ClientOs;
                            LogActivity.ClientBrowser = ClientBrowser;
                            LogActivity.ClientMemory = ClientMemory;
                            LogActivity.ClientCore = ClientCore;
                            try
                            {
                                _context.webLoginActivity.Add(LogActivity);
                                _context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                return Json(new { status = false, message = "Couldn't Get Login Information!" });
                                //throw;
                            }*/

                            return Json(new { status = true, message = "Login Successfull!", returnUrl = returnUrl, token = tokenString });
                            
                        }
                        else
                        {
                            return Json(new { status = false, message = "Invalid Password!" });
                        }
                    }
                    else
                    {
                        return Json(new { status = false, message = "User Not Found!" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Invalid UserName!" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid Login!" });
            }
        }


        //JWT AuthUser
        private SystemUsers AuthUsers(SystemUsers SystemUsers)
        {
            SystemUsers user = null;
            var getUser = _context.SystemUsers.Where(s => s.EMailAddress == SystemUsers.EMailAddress).FirstOrDefault();
            if ( getUser != null)
            {
                user = new SystemUsers { UserCode = getUser.UserCode, EMailAddress = getUser.EMailAddress };
            }
            return user;
        }

        //JWT Generate Token
        private string GenerateJSONWebToken(SystemUsers users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return (accessToken);
        }

        public ActionResult Logout()
        {
            var users = HttpContext.Session.GetString("EMailAddress");

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private string GenerateURL(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            if (menu.Length > 0)
            {
                foreach (DataRow dr in menu)
                {
                    var _SystemObject = _context.SystemObject.Where(s => s.SystemObjectID ==Convert.ToInt32(dr["ChildID"])).FirstOrDefault();
                    string objecttype = _SystemObject.ObjectType.ToString();
                    string menuText = _SystemObject.ObjectDesc.ToString();
                    string ControllerName = _SystemObject.ControllerName.ToString();
                    string ActionName = _SystemObject.ActionName.ToString();
                    string urllink = Url.Action(ActionName,ControllerName);
                    string icon = "zmdi zmdi-home";
                    if (objecttype != "Menu" || menuText == "LogOff")
                    {
                        string line = String.Format(@"<li class=""@Url.MakeActiveClass(""{0}"")""><a href = ""{0}""><i class=""{2}""></i> {1} </a></li>", urllink, menuText, icon);
                        //string lineAsli = String.Format(@"<li><a href=""{0}""><i class=""{2}""></i> <span>{1}</span></a></li>", url, menuText, icon);
                        sb.Append(line);
                    }
                    string pid = dr["ChildID"].ToString();
                    string parentId = dr["ParentId"].ToString();
                    DataRow[] subMenu = table.Select(String.Format("ParentId = '{0}'", pid));
                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {
                        string line = String.Format(@"<li class=""sub-menu @Url.MakeActiveClass(""{2}"")""><a href=""#"" data-ma-action=""submenu-toggle""><i class=""{0}""></i>{1}</a><ul>", icon, menuText, urllink);
                        var subMenuBuilder = new StringBuilder();
                        sb.AppendLine(line);
                        sb.Append(GenerateURL(subMenu, table, subMenuBuilder));
                        sb.Append("</ul></li>");
                    }
                }
            }
            return sb.ToString();
        }


        public DataSet ToDataSet<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dataTable);
            return ds;
        }

        private string GetFullTenant()
        {
            var thisLink = HttpContext.Request.GetDisplayUrl();
            var tenantActive = thisLink.Split("/")[3];
            return tenantActive;
        }
    }
}
