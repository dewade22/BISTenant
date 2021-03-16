using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Manufacturing.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Manufacturing.Models;

namespace Manufacturing.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Movement()
        {
            return View();
        }

        public IActionResult Valuation()
        {
            //Inven Report
            List<Manufacturing.Data.Entities.DimensionValue> cbInventRpt = _context.DimensionValue.Where(a => a.DimensionCode == "InventReport" && a.RowStatus == 0).OrderBy(a => a.DimensionValueName).ToList();
            ViewBag.cboInventRpt = new SelectList(cbInventRpt, "DimensionValueCode", "DimensionValueName");

            //Category
            List<Manufacturing.Data.Entities.ItemCategory> cbCategory = _context.ItemCategory.Where(a => a.RowStatus == 0).OrderBy(a => a.Code).ToList();
            ViewBag.cboCategory = new SelectList(cbCategory, "Code", "Description");

            //Product Group
            List<Manufacturing.Data.Entities.ProductGroup> cbProductGroups = _context.ProductGroup.Where(a => a.RowStatus == 0).OrderBy(a => a.Code).ToList();
            ViewBag.cboProductGroups = new SelectList(cbProductGroups, "Code", "Description");

            //Location
            List<Manufacturing.Data.Entities.Locations> cbLocation = _context.Locations.Where(a => a.RowStatus == 0).OrderBy(a => a.LocationCode).ToList();
            ViewBag.cboLocation = new SelectList(cbLocation, "LocationCode", "LocationName");

            //Flavour
            List<Manufacturing.Data.Entities.DimensionValue> cbFlavour = _context.DimensionValue.Where(a => a.DimensionCode == "Flavours" && a.RowStatus == 0).OrderBy(a => a.DimensionValueName).ToList();
            ViewBag.cboFlavour = new SelectList(cbFlavour, "DimensionValueCode", "DimensionValueName");

            //Size
            List<Manufacturing.Data.Entities.DimensionValue> cbSize = _context.DimensionValue.Where(a => a.DimensionCode == "Sizes" && a.RowStatus == 0).OrderBy(a => a.DimensionValueName).ToList();
            ViewBag.cboSize = new SelectList(cbSize, "DimensionValueCode", "DimensionValueName");

            return View();
        }

        public JsonResult ValuationData(ValuationFilter models)
        {
            var res = models;
            return new JsonResult(models);
        }
    }
}