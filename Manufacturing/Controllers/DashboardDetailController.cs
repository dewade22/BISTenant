using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manufacturing.Helpers;
using Manufacturing.Data;
using Manufacturing.Models.Items;

namespace Manufacturing.Controllers
{
    public class DashboardDetailController : Controller
    {
        private readonly ApplicationDbContext _appContext;

        public DashboardDetailController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        [AuthorizedAction]
        public IActionResult stock(string productGroup)
        {
            ViewBag.productGroup = productGroup;
            var data = (from ledger in _appContext.ItemLedgerEntry
                        join item in _appContext.Items on ledger.ItemNo equals item.ItemNo
                        where ledger.LocationCode.ToLower() == "warehouse" && item.ProductGroupCode.ToLower() == productGroup.ToLower()
                        group ledger by new { id = item.ItemNo, description = item.Description, unit = item.BaseUnitofMeasure } into result
                        select new RawItem { ItemNo = result.Key.id, Description = result.Key.description, BaseUnitofMeasure = result.Key.unit, Quantity = result.Sum(ledger => ledger.Quantity) }).ToList();
            return View(data);
        }

        [AuthorizedAction]
        public IActionResult Cukai()
        {
            var data = (from ledger in _appContext.ItemLedgerEntry
                        join item in _appContext.Items on ledger.ItemNo equals item.ItemNo
                        where item.ProductGroupCode.ToLower() == "pita" //ledger.LocationCode.ToLower() == "warehouse" &&
                        group ledger by new { id = item.ItemNo, description = item.Description, unit = item.BaseUnitofMeasure } into result
                        select new RawItem { ItemNo = result.Key.id, Description = result.Key.description, BaseUnitofMeasure = result.Key.unit, Quantity = result.Sum(ledger => ledger.Quantity) }).ToList();
            return View(data);
        }

    }
}
