using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Manufacturing.Models;
using Manufacturing.Models.Companies;
using Manufacturing.Data;
using Manufacturing.Domain.Multitenancy;
using Manufacturing.Domain.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Manufacturing.Domain.Extensions;

namespace Manufacturing.Controllers
{
    //[Route("/[controller]/[Action]/{client?}")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SystemDbContext _sysContext;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly ISettingsService _settingsService;
        private readonly IDistributedCache _distributedCache;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext applicationDbContext,
            SystemDbContext sysContext,
            IHttpContextAccessor httpContextAccessor,
            //ISettingsService settingsService,
            IDistributedCache distributedCache)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _sysContext = sysContext;
            _httpContextAccessor = httpContextAccessor;
            //_settingsService = settingsService;
            _distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            var company = (from companies in _sysContext.CompanyInformation
                           select new companyViewModel
                           {
                              CompanyCode = companies.CompanyCode,
                              CompanyName = companies.CompanyName
                           }).ToList();
            var Https = "";
            var Host = this.Request.Host;
            var isHttps = this.Request.IsHttps;
            if (isHttps == true)
            {
                Https = "https";
            }
            else
            {
                Https = "http";
            }
            ViewBag.Link = Https+"://"+Host;
            return View(company);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    [Serializable]
    public class Junk
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }
    }

    public class HomeIndexViewModel
    {
        public Client Client { get; set; }

        public Junk Junk { get; set; }
    }
}
