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
                result = "Terjadi Kesalahan Saat Mencoba Menambahkan Data";
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
                        result = "Gagal Saat Melakukan Perubahan Data";
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
        [HttpPut]
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

        [AuthorizedAction]
        public IActionResult FOHDetail(string ModelId)
        {
            if(ModelId == null || ModelId == "")
            {
                return View();
            }
            else
            {
                ViewBag.model = ModelId;
                var data = (from FOHBreakdown in _context.ModelDetailFOHBreakdown
                            join subProcess in _context.ModelSubProcess on FOHBreakdown.SPID equals subProcess.SubProcessId
                            join machine in _context.ModelMachineMaster on FOHBreakdown.SPMachineID equals machine.MachineNo into FM
                            from FOHMachine in FM.DefaultIfEmpty()
                            where (FOHBreakdown.Active == true && FOHBreakdown.ModelId == ModelId)
                            select new MachineViewModel
                            {
                                ModelDetailFOHBreakdown = FOHBreakdown,
                                ModelSubProcess = subProcess,
                                MachineMaster = FOHMachine
                            }).ToList();
                List<Manufacturing.Data.Entities.ModelMachineMaster> ListMachine = _context.ModelMachineMaster.Where(a => a.Active == true).OrderByDescending(a => a.MachineNo).ToList();
                ViewBag.ListMachine = new SelectList(ListMachine, "MachineNo", "MachineName");
                List<Manufacturing.Data.Entities.ModelSubProcess> SubProcess = _context.ModelSubProcess.Where(a => a.Active == true).OrderBy(a=>a.Id).ToList();
                ViewBag.listSubP = new SelectList(SubProcess, "SubProcessId", "SubProcessName");
                
                return View(data);
            }
        }

        [AuthorizedAPI]
        [HttpPost]
        public JsonResult FOHDetail(ModelDetailFOHBreakdown model)
        {
            var result = "";
            if(model == null)
            {
                result = "Server Tidak dapat menerima inputan";
            }
            else
            {
                model.ModelDetailFOHNo = GenerateFOHNo();
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = HttpContext.Session.GetString("EMailAddress");
                try
                {
                    _context.ModelDetailFOHBreakdown.Add(model);
                    _context.SaveChanges();
                    result = "sukses";
                }
                catch(Exception ex)
                {
                    result = "Terjadi Kesalahan saat menambahkan data "+ex;
                }
            }
            return Json(result);

        }

        [AuthorizedAPI]
        [HttpPut]
        public JsonResult FOHDetails(ModelDetailFOHBreakdown model)
        {
            var result = "";
            if(model == null)
            {
                result = "Data Baru tidak berhasil didapatkan";
            }
            else
            {
                var currentData = _context.ModelDetailFOHBreakdown.SingleOrDefault(a => a.ModelDetailFOHNo == model.ModelDetailFOHNo);
                if(currentData == null)
                {
                    result = "FOH dengan nomor "+model.ModelDetailFOHNo+" tidak ditemukan!";
                }
                else
                {
                    currentData.LastModifiedAt = DateTime.Now;
                    currentData.LastModifiedBy = HttpContext.Session.GetString("EMailAddress");
                    currentData.OperationName = model.OperationName;
                    currentData.SPDuration = model.SPDuration;
                    currentData.SPID = model.SPID;
                    currentData.SPMachineID = model.SPMachineID;
                    currentData.SPQuantity = model.SPQuantity;
                    currentData.SPSpeed = model.SPSpeed;
                    try
                    {
                        _context.ModelDetailFOHBreakdown.Update(currentData);
                        _context.SaveChanges();
                        result = "sukses";
                    }
                    catch(Exception ex)
                    {
                        result = "FOH gagal diubah dengan pesan kesalahan "+ex;
                    }
                }
            }
            return Json(result);
        }

        [AuthorizedAPI]
        [HttpGet]
        public JsonResult FOHDetails(string ModelId)
        {
            if (ModelId == null || ModelId == "")
            {
                return Json(false);
            }
            else
            {
                var data = _context.ModelDetailFOHBreakdown.Where(a => a.ModelDetailFOHNo == ModelId).SingleOrDefault();
                return Json(data);
            }
        }


        //Get Machine Spped DETAIL FOH B
        [AuthorizedAPI]
        [HttpGet]
        public JsonResult MachineSpeed(string machine)
        {
            if(machine != "")
            {
                var data = _context.ModelMachineMaster.Where(a => a.MachineNo == machine).Select(a => a.MachineSpeed);
                return Json(data);
            }
            return Json(false);
        }

        //Get Sub Process Size
        [AuthorizedAPI]
        [HttpGet]
        public JsonResult ProcessSize(string ModelId)
        {
            if(ModelId != "" || ModelId != null)
            {
                var data = _context.ModelDetailFOHBreakdown.Where(a => a.ModelId == ModelId).Select(a => a.SubProcessSize).FirstOrDefault();
                return Json(data);
            }
            return Json(false);
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
                         }).OrderByDescending(a=>a.MachineMaster.Id).ToList();

            List<Manufacturing.Data.Entities.ModelMachineType> MachineType = _context.ModelMachineType.Where(a => a.Active == true).OrderByDescending(a => a.Id).ToList();
            ViewBag.ListType = new SelectList(MachineType, "MachineTypeNo", "MachineTypeName");
            return View(model);
        }

        [AuthorizedAPI]
        [HttpPost]
        public JsonResult MesinMaster(ModelMachineMaster model)
        {
            var result = "";
            if(model == null)
            {
                result = "Server Tidak dapat menerima inputan";
            }
            else
            {
                var MesinMasterId = MachineMasterId();
                model.MachineNo = MesinMasterId;
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = HttpContext.Session.GetString("EMailAddress");
                try
                {
                    var insert = _context.ModelMachineMaster.Add(model);
                    _context.SaveChanges();
                    result = "sukses";
                }
                catch(Exception ex)
                {
                    result = "Terjadi Kesalahan Saat Menyimpan Data, " + ex;
                }
            }
            return Json(result);
        }

        [AuthorizedAPI]
        [HttpGet]
        public JsonResult MesinMasters(string MachineNo)
        {
            if(MachineNo == null || MachineNo == "")
            {
                return Json(false);
            }
            else
            {
                var data = _context.ModelMachineMaster.Where(a => a.MachineNo == MachineNo).SingleOrDefault();
                return Json(data);
            }
        }

        [AuthorizedAPI]
        [HttpPatch]
        public async Task<JsonResult> MesinMasters(ModelMachineMaster model)
        {
            var result = "Terjadi Kesalahan";
            if(model == null)
            {
                result = "Data Baru tidak berhasil didapatkan";
            }
            else
            {
                var currentData = _context.ModelMachineMaster.Where(a => a.MachineNo == model.MachineNo).SingleOrDefault();
                if(currentData == null)
                {
                    result = "Mesin Dengan Nomor " + model.MachineNo + " Tidak Ditemukan";
                }
                else
                {

                    currentData.MachineName = model.MachineName;
                    currentData.MachineType = model.MachineType;
                    currentData.MachinePrice = model.MachinePrice;
                    currentData.MachineSetupPrice = model.MachineSetupPrice;
                    currentData.MachineMaintenancePrice = model.MachineMaintenancePrice;
                    currentData.MaximumAgeUse = model.MaximumAgeUse;
                    currentData.SalvageValue = model.SalvageValue;
                    currentData.PowerConsumption = model.PowerConsumption;
                    currentData.MachineSpeed = model.MachineSpeed;
                    currentData.LastModifiedAt = DateTime.Now;
                    currentData.LastModifiedBy = HttpContext.Session.GetString("EMailAddress");

                    try
                    {
                        _context.Update(currentData);
                        await _context.SaveChangesAsync();
                        result = "sukses";
                    }
                    catch (Exception ex)
                    {
                        result = "Gagal saat melakukan perubahan data";
                        throw;
                    }
                    
                }
            }
            return Json(result);
        }

        [AuthorizedAPI]
        [HttpPatch]
        public JsonResult MesinMaster(string MachineNo)
        {
            if(MachineNo == "" || MachineNo == null)
            {
                return Json(false);
            }
            else
            {
                var data = _context.ModelMachineMaster.Where(a => a.MachineNo == MachineNo).SingleOrDefault();
                if(data == null)
                {
                    return Json(false);
                }
                else
                {
                    data.Active = false;
                    _context.ModelMachineMaster.Update(data);
                    _context.SaveChanges();
                    return Json(true);
                }
            }
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

        public string MachineMasterId()
        {
            string Id = "MCH-00001";
            var MaxId = _context.ModelMachineMaster.OrderByDescending(a => a.Id).Select(a => a.MachineNo).FirstOrDefault();
            if(MaxId != null)
            {
                char[] trimmed = { 'M', 'C', 'H', '-' };
                int currentIds = Convert.ToInt32(MaxId.Trim(trimmed));
                if(currentIds < 10)
                {
                    Id = "MCH-0000"+(currentIds +1);
                }
                else if(currentIds < 100){
                    Id = "MCH-000" + (currentIds + 1);
                }
                else if(currentIds < 1000)
                {
                    Id = "MCH-00" + (currentIds + 1);
                }
                else if (currentIds < 10000)
                {
                    Id = "MCH-0" + (currentIds + 1);
                }
                else
                {
                    Id = "MCH-" + (currentIds + 1);
                }
            }
            return Id;
        }

        public string GenerateFOHNo()
        {
            string Id = "FOHB-00001";
            var MaxId = _context.ModelDetailFOHBreakdown.OrderByDescending(a => a.Id).Select(a => a.ModelDetailFOHNo).FirstOrDefault();
            if (MaxId != null)
            {
                char[] trimmed = { 'F', 'O', 'H', 'B', '-' };
                int currentIds = Convert.ToInt32(MaxId.Trim(trimmed));
                if (currentIds < 10)
                {
                    Id = "FOHB-0000" + (currentIds + 1);
                }
                else if (currentIds < 100)
                {
                    Id = "FOHB-000" + (currentIds + 1);
                }
                else if (currentIds < 1000)
                {
                    Id = "FOHB-00" + (currentIds + 1);
                }
                else if (currentIds < 10000)
                {
                    Id = "FOHB-0" + (currentIds + 1);
                }
                else
                {
                    Id = "FOHB-" + (currentIds + 1);
                }
            }
            return Id;
        }
    }
}