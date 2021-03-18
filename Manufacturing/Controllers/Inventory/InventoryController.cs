using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Manufacturing.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Manufacturing.Models;
using Manufacturing.Helpers;
using Microsoft.Data.SqlClient;
using System.Data;
using Manufacturing.Data.Entities;
using Microsoft.EntityFrameworkCore;

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

        [AuthorizedAction]
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

        public IActionResult ValuationData(ValuationFilter models)
        {
            var res = models;
            var parameters = new[]
           {
                new SqlParameter("@StartingPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = res.startDate},
                new SqlParameter("@EndingPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = res.endDate},
                new SqlParameter("@prmInventory", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = ""},
                new SqlParameter("@prmCategory", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = res.category},
                new SqlParameter("@prmProductGroup", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = res.prodGroups},
                new SqlParameter("@prmSizes", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = res.Size},
                new SqlParameter("@prmFlavour", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = res.Flavour},
                new SqlParameter("@prmInventRpt", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = res.invenRPT},
                new SqlParameter("@prmLocation", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = res.location},
                new SqlParameter("@ItemNo", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = ""},
            };

            //Define Model
            List<spRptInventoryValuationModel> Valuation = new List<spRptInventoryValuationModel>();
            try
            {
                Valuation = _context.SpRptInventoryValuationModel.FromSqlRaw(@"exec spRptInventoryValuation @StartingPeriod, @EndingPeriod, @prmInventory, @prmCategory, @prmProductGroup, @prmSizes, @prmFlavour, @prmInventRpt, @prmLocation, @ItemNo", parameters).AsNoTracking().ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return new JsonResult(Valuation);
        }
    }
}