using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Manufacturing.Data;
using Manufacturing.Helpers;
using Manufacturing.Data.Entities;

namespace Manufacturing.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext _appContext;
        public DashboardController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        [AuthorizedAction]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizedAPI]
        public IActionResult getSpirit(string tenant)
        {
            if (tenant.ToLower() == "balimoon" || tenant.ToLower() == "training" || tenant.ToLower().Contains("bml"))
            {
                var result = (from itemLedger in _appContext.ItemLedgerEntry
                             join items in _appContext.Items
                             on itemLedger.ItemNo equals items.ItemNo
                             where itemLedger.LocationCode == "WHFG" && items.InventoryPostingGroup == "FGInTrans"
                             group itemLedger by new { items.ProductGroupCode, items.Attrib1Code } into res
                             select new { productGroup = res.Key.ProductGroupCode, kemasan = res.Key.Attrib1Code, Quantity = res.Sum(a => a.Quantity) }).ToList();
                return new JsonResult(result);
            }
            else if(tenant.ToLower().Contains("bmi"))
            {
                var result = (from itemledger in _appContext.ItemLedgerEntry
                              join item in _appContext.Items
                              on itemledger.ItemNo equals item.ItemNo
                              where !item.Description.Contains("Dummy")
                              group itemledger by new { item.ItemCategoryCode, item.DimensionValue01Code, item.ProductGroupCode } into res
                              select new { productGroup = res.Key.ItemCategoryCode, kemasan = res.Key.DimensionValue01Code, res.Key.ProductGroupCode, Quantity = res.Sum(a => a.Quantity) }).ToList();
                return new JsonResult(result);
            }
            else
            {
                var result = (from itemledger in _appContext.ItemLedgerEntry
                              join item in _appContext.Items
                              on itemledger.ItemNo equals item.ItemNo
                              where item.ItemCategoryCode == "FG"
                              group itemledger by new { item.ProductGroupCode, item.LiterQty } into res
                              select new { productGroup = res.Key.ProductGroupCode, kemasan = res.Key.LiterQty, Quantity = res.Sum(a => a.Quantity) }).ToList();
                return new JsonResult(result);
            }
            
        }

        [AuthorizedAPI]
        public IActionResult getLiq(string tenant)
        {
            if(tenant.ToLower() == "balimoon" || tenant.ToLower() == "training" || tenant.ToLower().Contains("bml"))
            {
                var result = (from itemledger in _appContext.ItemLedgerEntry
                              join items in _appContext.Items
                              on itemledger.ItemNo equals items.ItemNo
                              where itemledger.LocationCode == "WHFG" && items.InventoryPostingGroup == "FGInTrans" && items.ProductGroupCode == "Liqueurs"
                              group itemledger by new { liqGroup = items.DimensionValue02Code, kemasan = items.Attrib1Code } into res
                              select new { res.Key.liqGroup, res.Key.kemasan, quantity = (res.Sum(res => res.Quantity))}).ToList();
                return new JsonResult(result);
            }
            else if (tenant.ToLower().Contains("bmi"))
            {
                var result = (from itemledger in _appContext.ItemLedgerEntry
                              join items in _appContext.Items
                              on itemledger.ItemNo equals items.ItemNo
                              where items.ItemCategoryCode == "Liqueurs" && !items.DimensionValue03Code.Contains("Ceramic") && !items.DimensionValue03Code.Contains("Cramic") && !items.Description.Contains("Dummy")
                              group itemledger by new { liqGroup = items.DimensionValue02Code, kemasan = items.LiterQty } into res
                              select new { res.Key.liqGroup, res.Key.kemasan, quantity = (res.Sum(res => res.Quantity)) }).ToList();
                return new JsonResult(result);
            }
            else if(tenant.ToLower().Contains("bip"))
            {
                var result = (from itemledger in _appContext.ItemLedgerEntry
                              join item in _appContext.Items
                              on itemledger.ItemNo equals item.ItemNo
                              where item.ItemCategoryCode == "FG" && item.ProductGroupCode == "Liqueurs" && item.DimensionValue03Code != "Cramic"
                              group itemledger by new { item.ItemNo, item.LiterQty } into res
                              select new { productGroup = res.Key.ItemNo, kemasan = res.Key.LiterQty, Quantity = res.Sum(a => a.Quantity) }).ToList();
                return new JsonResult(result);
            }
            else
            {
                var result = "";
                return new JsonResult(result);
            }
        }

        [AuthorizedAPI]
        public ActionResult DataSales(int tahun)
        {
            string[] dates = new string[12] { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };
            var months = Enumerable.Range(1, 12);
            var lastYear = tahun - 1;
            //sales invoice = real dan itembudget entry = forecast
            //item Budget Entry
            var forecast = (from a in _appContext.ItemBudgetEntry
                            join item in _appContext.Items on a.ItemNo equals item.ItemNo
                            where a.Date.Value.Year == tahun
                            group new { a, item } by new { bulan = a.Date.Value.Month } into c
                            select new { c.Key.bulan, jumlah = c.Sum(a => a.a.Quantity * a.item.UnitPrice) }).ToList();

            string[] sumForecast = (from month in months
                                    from a in forecast.Where(a => month == a.bulan).DefaultIfEmpty()
                                    select a == null ? "0" : a.jumlah.ToString()).ToArray();

            var invoices = (from a in _appContext.SalesInvoiceLine
                            join b in _appContext.Items on a.RecordNo equals b.ItemNo
                            where a.ShipmentDate.Value.Year == tahun
                            group new { a, b } by new{ bulan = a.ShipmentDate.Value.Month } into c
                            select new { c.Key.bulan, jumlah = c.Sum(a => a.a.Quantity * a.b.UnitPrice ) }).ToList();

            string[] sumInvoices = (from month in months
                                    from a in invoices.Where(a => month == a.bulan).DefaultIfEmpty()
                                    select a == null ? "0" : a.jumlah.ToString()).ToArray();

            var lastyearInvoces = (from a in _appContext.SalesInvoiceLine
                                   where a.ShipmentDate.Value.Year == lastYear
                                   join b in _appContext.Items on a.RecordNo equals b.ItemNo
                                   group new { a, b } by new { bulan = a.ShipmentDate.Value.Month } into c
                                   select new { c.Key.bulan, jumlah = c.Sum(a => a.a.Quantity * a.b.UnitPrice) }).ToList();

            string[] sumLastYear = (from month in months
                                    from a in lastyearInvoces.Where(a => month == a.bulan).DefaultIfEmpty()
                                    select a == null ? "0" : a.jumlah.ToString()).ToArray();


            return new JsonResult(new { label = dates, data = sumForecast, invoice = sumInvoices, lastYear = sumLastYear });
        }

        [HttpGet]
        [AuthorizedAPI]
        public ActionResult getStock(string productGroup)
        {
            var result = (from itemLedger in _appContext.ItemLedgerEntry
                          join item in _appContext.Items on itemLedger.ItemNo equals item.ItemNo
                          where itemLedger.LocationCode.ToLower() == "warehouse" && item.ProductGroupCode.ToLower() == productGroup.ToLower()
                          group itemLedger by new { item.BaseUnitofMeasure } into res
                          select new { unit = res.Key.BaseUnitofMeasure, quantity = res.Sum(a => a.Quantity) }).ToList();
            
            return new JsonResult(result);
        }

        [HttpGet]
        [AuthorizedAPI]
        public IActionResult getCukai()
        {
            var result = (from itemledger in _appContext.ItemLedgerEntry
                          join item in _appContext.Items on itemledger.ItemNo equals item.ItemNo
                          where item.ProductGroupCode.ToLower() == "pita" // itemledger.LocationCode.ToLower() == "warehouse" &&
                          group itemledger by new { item.BaseUnitofMeasure } into res
                          select new { unit = res.Key.BaseUnitofMeasure, quantity = res.Sum(a => a.Quantity) }).ToList();
            return new JsonResult(result);
        }

        [AuthorizedAction]
        public IActionResult wizard()
        {
            return View();
        }

        [AuthorizedAction]
        public IActionResult TemplateList()
        {
            return View("Templates/TemplateList");
        }

        [AuthorizedAction]
        [HttpPost]
        public string CreateDashboard(Dashboards_Info dashboard)
        {
            var get = _appContext.Dashboards_Info.FirstOrDefault(a => a.id == dashboard.id && a.Name == dashboard.Name);
            if (get == null)
            {
                //Insert
                try
                {
                    //_appContext.Dashboards_Info.Add(dashboard);
                    var save = new Dashboards_Info();
                    save.id = 0;
                    save.Name = dashboard.Name;
                    save.TemplateId = dashboard.TemplateId;
                    _appContext.Dashboards_Info.Add(save);
                    _appContext.SaveChanges();
                    return save.id.ToString();
                }
                catch (System.Exception)
                {
                    return "False";
                }
            }
            else
            {
                //Update
                try
                {
                    get.TemplateId = dashboard.TemplateId;
                    _appContext.Update(get);
                    _appContext.SaveChanges();
                    return dashboard.id.ToString();
                }
                catch (System.Exception)
                {
                    return "False";
                }
            }         
        }

        [AuthorizedAction]
        public IActionResult Dashboard(int id)
        {
            Dashboards_Info dashboards = _appContext.Dashboards_Info.Where(a => a.id == id).FirstOrDefault();
            int elementCount = _appContext.Templates.Where(a => a.id == dashboards.TemplateId).Select(a => a.elementCount).FirstOrDefault();
            var linkedElements = _appContext.DashboardLinkedElements.Where(a => a.DashboardId == id).ToList();
            for(int i=1; i<=elementCount; i++)
            {
                var element = linkedElements.Where(a => a.Placement == i.ToString());
                if (element.Any())
                {
                    ViewData["Element" + i] = "../Elements/Element" + element.First().ElementId + ".cshtml";
                }
                else
                {
                    ViewData["Element" + i] = "../Elements/Default.cshtml";
                }
            }
            ViewData["dashboard-name"] = dashboards.Name;
            ViewData["dashboardId"] = id;
            return View("Templates/Template" + dashboards.TemplateId);
        }
        [AuthorizedAction]
        public IActionResult ElementList(int id)
        {
            ViewData["dashboardId"] = id;
            return View("Elements/ElementList");
        }

        public string AddElement(DashboardLinkedElements element)
        {

            var old = _appContext.DashboardLinkedElements.Where(s => s.DashboardId == element.DashboardId && s.Placement == element.Placement).ToList();
            foreach (var item in old)
            {
                _appContext.DashboardLinkedElements.Remove(item);
            }
            _appContext.SaveChanges();

            try
            {
                _appContext.DashboardLinkedElements.Add(element);
                _appContext.SaveChanges();
                return "True";
            }
            catch (System.Exception e)
            {
                return e.Message;
            }
        }

        public ActionResult GetDashboardsList()
        {
            return Ok(_appContext.Dashboards_Info.ToList());
        }
    }
}