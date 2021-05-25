using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Manufacturing.Helpers;
using Manufacturing.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Manufacturing.Models.Hpp;
using Manufacturing.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

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
            List<Manufacturing.Data.Entities.Items> MMEA = _context.Items.Where(a => a.RowStatus == 0 && a.InventoryPostingGroup == "MMEA").ToList();
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
            var models = (from items in _context.Items
                          join model in _context.ModelMaster
                          on items.ItemNo equals model.ProductID_SKUID
                          where model.Active == true
                          select new ModelMasterViewModel
                          {
                              itemTable = items,
                              masterModel = model
                          });
            return View(models);
        }

        [AuthorizedAction]
        public IActionResult RegisterModel()
        {
            var ids = RegisterModelId();
            ViewData["id"] = ids;
            List<Manufacturing.Data.Entities.Items> MMEA = _context.Items.Where(a => a.RowStatus == 0 && a.InventoryPostingGroup == "FGInTrans").ToList();
            ViewBag.ListMMEA = new SelectList(MMEA, "ItemNo", "Description");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterModel(ModelMaster model)
        {
            model.ModelId = RegisterModelId();
            if (ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = HttpContext.Session.GetString("EMailAddress");
                model.Active = true;
                try
                {
                    var result = await _context.ModelMaster.AddAsync(model);
                    _context.SaveChanges();

                    return RedirectToAction("MasterModel", "HPPItem");
                }catch(Exception)
                {
                    throw;
                }   
            }
            RegisterModel();
            return View();
        }

        [AuthorizedAction]
        public IActionResult EditModels(string ModelId)
        {
            var models = new ModelMaster();
            if(ModelId != null)
            {
                try
                {
                    List<Manufacturing.Data.Entities.Items> MMEA = _context.Items.Where(a => a.RowStatus == 0 && a.InventoryPostingGroup == "FGInTrans").ToList();
                    ViewBag.ListMMEA = new SelectList(MMEA, "ItemNo", "Description");
                    models = _context.ModelMaster.Where(a => a.ModelId == ModelId).FirstOrDefault();
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> EditModels(ModelMaster model)
        {
            if (ModelState.IsValid)
            {
                var getModels = _context.ModelMaster.Where(a => a.ModelId == model.ModelId).SingleOrDefault();
               if(getModels != null)
                {
                    getModels.LastModifiedAt = DateTime.Now;
                    getModels.ModelName = model.ModelName;
                    getModels.ProductID_SKUID = model.ProductID_SKUID;
                    getModels.VersionNo = model.VersionNo;
                    getModels.Description = model.Description;
                    try
                    {
                        var result = _context.ModelMaster.Update(getModels);
                        var save = await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        EditModels(model.ModelId);
                        return RedirectToAction("EditModels", new { ModelId = model.ModelId });
                    }
                }
                
            }
            return RedirectToAction("MasterModel");
        }

        public string RegisterModelId()
        {
            string id = "MM-00001";
            var MaxId = _context.ModelMaster.OrderByDescending(a => a.Id).Select(a => a.ModelId).FirstOrDefault();
            if (MaxId == null)
            {
                id = "MM-00001";
            }
            else
            {
                char[] trimmed = { 'M', '-' };
                int currentIds = Convert.ToInt32(MaxId.Trim(trimmed));
                if(currentIds+1 < 10)
                {
                    id = "MM-0000" + (currentIds + 1);
                }
                else if(currentIds+1 < 100)
                {
                    id = "MM-000" + (currentIds + 1);
                }
                else if (currentIds + 1 < 1000)
                {
                    id = "MM-00" + (currentIds + 1);
                }
                else if(currentIds + 1 < 10000)
                {
                    id = "MM-0" + (currentIds + 1);
                }
                else
                {
                    id = "MM-"+ (currentIds + 1);
                }
            }
            return id;
        }
    }
}