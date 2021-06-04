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
using Newtonsoft.Json;

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
            List<Manufacturing.Data.Entities.Items> MMEA = _context.Items.Where(a => a.RowStatus == 0 && a.Blocked == 0 && a.InventoryPostingGroup == "MMEA").ToList();
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
                    getModels.LastModifiedBy = HttpContext.Session.GetString("EMailAddress");
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

        public Boolean DeleteModel(string id)
        {
            if(id == null)
            {
                return false;
            }
            else
            {
                try
                {
                    var data = _context.ModelMaster.Where(a => a.ModelId == id).SingleOrDefault();
                    data.Active = false;
                    var result = _context.ModelMaster.Update(data);
                    var save = _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    //throw;
                    return false;
                }
            }            
        }

        [AuthorizedAction]
        public IActionResult DetailMaterial()
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
        public IActionResult MasterDetail(string ModelId)
        {
            if(ModelId == null)
            {
                ViewBag.ErrorMessage = "Model ID Null";
                return View();
            }
            else
            {
                //Cek ID Exist
                var cek = _context.ModelMaster.Where(a => a.ModelId == ModelId);
                if(cek == null)
                {
                    ViewBag.ErrorMessage = "Model ID Not found";
                    return View();
                }
                else
                {
                    var models = (from master in _context.ModelMaster
                                  join detail in _context.ModelDetailMaterial
                                  on master.ModelId equals detail.ModelId
                                  join item in _context.Items
                                  on detail.MatID equals item.ItemNo
                                  where detail.ModelId == ModelId && detail.Active == true
                                  select new ModelMasterDetailMaterialVM
                                  {
                                      masterModel = master,
                                      detailMaterial = detail,
                                      Items = item
                                  }).ToList();

                    //select row mats
                    List<Manufacturing.Data.Entities.Items> Materials = _context.Items.Where(a => a.Blocked == 0 && a.RowStatus ==0).OrderByDescending(a=>a.ItemId).ToList();
                    ViewBag.listMaterial = new SelectList(Materials, "ItemNo", "Description");
                    ViewBag.modelId = ModelId;
                    var detNo = models.Select(a => a.detailMaterial.ModelDetailNo).FirstOrDefault();
                    if(detNo == null)
                    {
                        ViewBag.ModelDetNo = "0";
                    }
                    else
                    {
                        ViewBag.ModelDetNo = detNo.ToString();
                    }
                    return View(models);
                }                
            }
        }

        [AuthorizedAPI]
        [HttpPost]
        public JsonResult MasterDetail(ModelDetailMaterial request)
        {
            var result = "Terjadi Masalah saat penambahan";
            request.CreatedAt = DateTime.Now;
            request.CreatedBy = HttpContext.Session.GetString("EMailAddress");
            if (request.ModelDetailNo == "0")
            {
                request.ModelDetailNo = MasterDetailID();
            }
            try
            {
                var insert = _context.ModelDetailMaterial.Add(request);
                _context.SaveChanges();
                result = "sukses";
            }
            catch (Exception e)
            {
                throw;
            }
            return Json(result);
        }

        [AuthorizedAPI]
        [HttpGet]
        public JsonResult MasterDetailEdit(int? id)
        {
            //string result = null;
            
            if(id != null)
            {
                var data = _context.ModelDetailMaterial.Where(a => a.Id == id).SingleOrDefault();
                return Json(data);
            }
            return Json(false);
        }

        [AuthorizedAPI]
        [HttpPut]
        public JsonResult MasterDetailEdit(ModelDetailMaterial request)
        {
            var result = "";
            if(request == null)
            {
                result = "Data Baru tidak berhasil didapatkan";
            }
            else
            {
                var currentData = _context.ModelDetailMaterial.Where(a => a.Id == request.Id).SingleOrDefault();
                if(currentData == null)
                {
                    result = "Data dengan Id "+request.Id+" Tidak ditemukan pada sistem";
                }
                else
                {
                    currentData.LastModifiedAt = DateTime.Now;
                    currentData.LastModifiedBy = HttpContext.Session.GetString("EMailAddress");
                    currentData.MatID = request.MatID;
                    currentData.QtyMatID = request.QtyMatID;
                    try
                    {
                        _context.ModelDetailMaterial.Update(currentData);
                        _context.SaveChanges();
                        result = "sukses";
                    }
                    catch(Exception ex)
                    {
                        result = "Gagal Saat Menambahkan Data";
                        throw;
                    }                    
                }                
            }
            return Json(result);
        }

        [AuthorizedAPI]
        [HttpGet]
        public JsonResult GetUnitItem(string ItemNo)
        {
            if(ItemNo != "" || ItemNo != null)
            {
                var data = _context.Items.Where(a => a.ItemNo == ItemNo).Select(a=>a.BaseUnitofMeasure).FirstOrDefault();
                return Json(data);
            }
            else
            {
                return Json(false);
            }
        }

        [AuthorizedAPI]
        [HttpDelete]
        public JsonResult DeleteMaterial(int? id)
        {
            if(id != null)
            {
                var current = _context.ModelDetailMaterial.Where(a => a.Id == id).SingleOrDefault();
                if(current != null)
                {
                    current.LastModifiedAt = DateTime.Now;
                    current.LastModifiedBy = HttpContext.Session.GetString("EMailAddress");
                    current.Active = false;
                    try
                    {
                        _context.ModelDetailMaterial.Update(current);
                        _context.SaveChanges();
                        return Json(true);
                    }
                    catch(Exception ex)
                    {
                        return Json(false);
                        throw;
                    }
                }
                
            }
            return Json(false);
        }

        /*Model Detail FOH Breakdown*/

        [AuthorizedAction]
        public IActionResult FOHMaster()
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



        /*Untuk Penambahan Mesin*/
        [AuthorizedAction]
        public IActionResult MesinMaster()
        {
            var model = (from machine in _context.ModelMachineMaster
                         join type in _context.ModelMachineType
                         on machine.MachineType equals type.MachineTypeNo
                         where machine.Active == true
                         select new MachineViewModel
                         {
                             MachineMaster = machine,
                             MachineType = type
                         }).ToList();

            List<Manufacturing.Data.Entities.ModelMachineType> MachineType = _context.ModelMachineType.Where(a => a.Active == true).OrderByDescending(a => a.Id).ToList();
            ViewBag.ListType = new SelectList(MachineType, "MachineTypeNo", "MachineTypeName");
            return View(model);
        }














        /*Auto Number ID*/
        public string MasterDetailID()
        {
            string ID = "MAT-00001";
            var MaxID = _context.ModelDetailMaterial.OrderByDescending(a => a.Id).Select(a => a.ModelDetailNo).FirstOrDefault();
            if(MaxID != null)
            {
                char[] trimmed = { 'M', 'A', 'T', '-' };
                int currentIds = Convert.ToInt32(MaxID.Trim(trimmed));
                if (currentIds + 1 < 10)
                {
                    ID = "MAT-0000" + (currentIds + 1);
                }
                else if (currentIds + 1 < 100)
                {
                    ID = "MAT-000" + (currentIds + 1);
                }
                else if (currentIds + 1 < 1000)
                {
                    ID = "MAT-00" + (currentIds + 1);
                }
                else if (currentIds + 1 < 10000)
                {
                    ID = "MAT-0" + (currentIds + 1);
                }
                else
                {
                    ID = "MAT-" + (currentIds + 1);
                }
            }
            return (ID);
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
                if (currentIds + 1 < 10)
                {
                    id = "MM-0000" + (currentIds + 1);
                }
                else if (currentIds + 1 < 100)
                {
                    id = "MM-000" + (currentIds + 1);
                }
                else if (currentIds + 1 < 1000)
                {
                    id = "MM-00" + (currentIds + 1);
                }
                else if (currentIds + 1 < 10000)
                {
                    id = "MM-0" + (currentIds + 1);
                }
                else
                {
                    id = "MM-" + (currentIds + 1);
                }
            }
            return id;
        }
    }
}