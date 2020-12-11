using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Manufacturing.Data;
using Manufacturing.Models;
using System.Data;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Manufacturing.Domain.Data;
using Manufacturing.Domain.Multitenancy;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Extensions;

namespace Manufacturing.Controllers
{
    public class AccountController : Controller
    {
        private readonly SystemDbContext _context;
        private readonly ApplicationDbContext _ApplicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(
            SystemDbContext context,
            ApplicationDbContext applicationDbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _ApplicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Validate(SystemUsers SystemUsers, string returnUrl = null)
        {
           var client = _httpContextAccessor.HttpContext.GetClient();
            if (client != null)
            {
                if (SystemUsers.EMailAddress != "" && SystemUsers.EMailAddress != null)
                {
                    SystemUsers _SystemsUsers = _context.SystemUsers.Where(s => s.EMailAddress == SystemUsers.EMailAddress).FirstOrDefault();
                    if (_SystemsUsers != null)
                    {
                        string userpass = dbSecurity.MD5(SystemUsers.UserPassword);
                        if (_SystemsUsers.UserPassword == userpass)
                        {
   
                            HttpContext.Session.SetString("EMailAddress", _SystemsUsers.EMailAddress);
                            HttpContext.Session.SetString("UserCode", _SystemsUsers.UserCode);
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

                            return Json(new { status = true, message = "Login Successfull!", returnUrl = returnUrl });
                            //var model = SystemUserMenus;
                            //return View("Views/Home/Index.cshtml",model);
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


        public ActionResult Logout()
        {
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
                        string line = String.Format(@"<li class=""sub-menu""><a href=""#"" data-ma-action=""submenu-toggle""><i class=""{0}""></i>{1}</a><ul>", icon, menuText);
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
            return thisLink;
        }
    }
}
