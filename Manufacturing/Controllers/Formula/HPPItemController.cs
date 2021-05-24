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
        public IActionResult Index()
        {
            Formula();
            return View("/Views/HPPItem/Formula.cshtml");
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

        public string GetBOMName(string BoMId)
        {
            string BoMName = _context.ProductionBomheader.Where(a => a.ProductionBomheaderNo == BoMId).Select(a => a.Description).FirstOrDefault();
            return BoMName;
        }

        [AuthorizedAction]
        public IActionResult PraMixing(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = GetBOMName(BoMId);
            return View();
        }
        
        [AuthorizedAction]
        public IActionResult Mixing(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = GetBOMName(BoMId);
            return View();
        }

        [AuthorizedAction]
        public IActionResult Aging(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = GetBOMName(BoMId);
            return View();
        }

        [AuthorizedAction]
        public IActionResult PostAging(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = GetBOMName(BoMId);
            return View();
        }

        [AuthorizedAction]
        public IActionResult CuciBotol(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = GetBOMName(BoMId);
            return View();
        }

        [AuthorizedAction]
        public IActionResult Bottling(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = GetBOMName(BoMId);
            return View();
        }

        [AuthorizedAction]
        public IActionResult Labeling(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = GetBOMName(BoMId);
            return View();
        }

        [AuthorizedAction]
        public IActionResult PackingBox(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = GetBOMName(BoMId);
            return View();
        }

        [AuthorizedAction]
        public IActionResult PackingCukai(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = GetBOMName(BoMId);
            return View();
        }

        [AuthorizedAction]
        public IActionResult Summary(string BoMId)
        {
            ViewBag.BomId = BoMId;
            ViewBag.BomName = GetBOMName(BoMId);
            return View();
        }

        [AuthorizedAction]
        public IActionResult MasterModel()
        {
            var models = _context.ModelMaster.ToList();
            return View(models);
        }
    }
}