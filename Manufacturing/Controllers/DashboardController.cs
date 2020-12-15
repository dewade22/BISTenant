using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Manufacturing.Data;

namespace Manufacturing.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext _appContext;
        public DashboardController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult getSpirit(string tenant)
        {
            if (tenant == "Balimoon")
            {
                var result = (from itemLedger in _appContext.ItemLedgerEntry
                             join items in _appContext.Items
                             on itemLedger.ItemNo equals items.ItemNo
                             where itemLedger.LocationCode == "WHFG" && items.InventoryPostingGroup == "FGInTrans"
                             group itemLedger by new { items.ProductGroupCode, items.Attrib1Code } into res
                             select new { res.Key.ProductGroupCode, res.Key.Attrib1Code, Quantity = res.Sum(a => a.Quantity) }).ToList();
                return new JsonResult(result);
            }
            else
            {
                var result = (from itemledger in _appContext.ItemLedgerEntry
                              join item in _appContext.Items
                              on itemledger.ItemNo equals item.ItemNo
                              group itemledger by new { item.ItemCategoryCode, item.DimensionValue01Code } into res
                              select new { res.Key.ItemCategoryCode, res.Key.DimensionValue01Code, Quantity = res.Sum(a => a.Quantity) }).ToList();
                return new JsonResult(result);
            }
            
        }

    }
}