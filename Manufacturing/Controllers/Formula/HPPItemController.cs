using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Manufacturing.Helpers;
using Manufacturing.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Manufacturing.Controllers
{
    public class HPPItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HPPItemController(ApplicationDbContext context)
        {
            _context = context;
        }


        [AuthorizedAction]
        public IActionResult Formula()
        {
            List<Manufacturing.Data.Entities.Items> MMEA = _context.Items.Where(a => a.RowStatus == 0 && a.Description.Contains("MMEA")).ToList();
            ViewBag.ListMMEA = new SelectList(MMEA, "ItemNo", "Description");
            return View();
        }
        [AuthorizedAPI]
        [HttpGet]
        public JsonResult selectBOMLine(string productId)
        {
            var BomLine = (from head in _context.ProductionBomheader
                           join line in _context.ProductionBomline
                           on head.ProductionBomheaderNo equals line.ProductionBomno
                           where line.RecordNo == productId
                           select new { 
                                value = head.ProductionBomheaderNo,
                                text = head.Description
                           }).ToList();
           
            return new JsonResult(BomLine);
        }
        [AuthorizedAction]
        public IActionResult PraMixing(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = _context.ProductionBomheader.Where(a => a.ProductionBomheaderNo == BoMId).Select(a=>a.Description).FirstOrDefault();
            return View();
        }
        [AuthorizedAction]
        public IActionResult Mixing(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = _context.ProductionBomheader.Where(a => a.ProductionBomheaderNo == BoMId).Select(a => a.Description).FirstOrDefault();
            return View();
        }

        [AuthorizedAction]
        public IActionResult Aging(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = _context.ProductionBomheader.Where(a => a.ProductionBomheaderNo == BoMId).Select(a => a.Description).FirstOrDefault();
            return View();
        }
    }
}