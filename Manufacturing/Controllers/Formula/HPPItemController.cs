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
            var data = new ModelMasterVM();
            var ListMaster = (from master in _context.ModelMaster
                              join item in _context.Items
                              on master.ProductID_SKUID equals item.ItemNo
                              where master.Active == true
                              select new ModelMasterViewModel
                              {
                                  itemTable = item,
                                  masterModel = master
                              }).ToList();
            data.ListMaster = ListMaster;
            return View(data);
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

        public string GetBOMName(string ModelId)
        {
            string BoMName = (from master in _context.ModelMaster
                              join item in _context.Items
                              on master.ProductID_SKUID equals item.ItemNo
                              where master.ModelId == ModelId
                              select new ModelMasterViewModel
                              {
                                  itemTable = item,
                                  masterModel = master
                              }).Select(a=>a.itemTable.Description).FirstOrDefault();
            return BoMName;
        }

        [AuthorizedAction]
        public IActionResult ListPraMixing()
        {
            var data = new ModelMasterVM();
            data.ListWipHeader = _context.ModelWIPProcessHeader.ToList();
            return View(data);
        }

        [AuthorizedAction]
        public IActionResult PraMixing()
        {
            var data = new ModelMasterVM();
            data.ListItems = _context.Items.Where(a => a.RowStatus == 0).ToList();
            List<Manufacturing.Data.Entities.UnitOfMeasures> Unit = _context.UnitOfMeasures.Where(a => a.RowStatus == 0).ToList();
            ViewBag.Unit = new SelectList(Unit, "UOMCode", "UOMDescription");
            return View(data);
        }

        public JsonResult SaveHeaderPraMixing(ModelWIPProcessHeader model)
        {
            var result = "";
            model.ModelHeaderId = GenerateWIPHeaderID();
            model.CreatedAt = DateTime.Now;
            model.CreatedBy = HttpContext.Session.GetString("EMailAddress");
            try
            {
                _context.ModelWIPProcessHeader.Add(model);
                _context.SaveChanges();
                result = "sukses";
            }
            catch(Exception ex)
            {
                result = ex.ToString();
            }

            return Json(new { result = result, ModelId = model.ModelHeaderId });
        }

        [AuthorizedAction]
        public IActionResult PraMixings(string Header)
        {
            var data = new ModelPraMixing();
            data.header = _context.ModelWIPProcessHeader.Where(a => a.ModelHeaderId == Header).FirstOrDefault();
            data.listLine = _context.ModelWIPProcessLine.Where(a => a.ModelWIPHeaderId == Header && a.Active == true).ToList();
            List<Manufacturing.Data.Entities.ModelRateType> RateList = _context.ModelRateType.Where(a => a.Active == true).ToList();
            List<Manufacturing.Data.Entities.UnitOfMeasures> Unit = _context.UnitOfMeasures.Where(a => a.RowStatus == 0).ToList();
            ViewBag.Unit = new SelectList(Unit, "UOMCode", "UOMDescription");
            ViewBag.RateList = new SelectList(RateList, "RateTypeCode", "RateTypeName");
            return View(data);
        }

        [AuthorizedAction]
        public IActionResult Mixing(string ModelId)
        {
            ViewBag.ModelId = ModelId;
            ViewBag.Product = GetBOMName(ModelId);
            var data = new ModelMixing();
            data.master = _context.ModelMaster.Where(a => a.ModelId == ModelId).FirstOrDefault();
            data.fOHBreakdown = _context.ModelDetailFOHBreakdown.Where(a => a.ModelId == ModelId).FirstOrDefault();
            data.detailProcessHeader = _context.ModelDetailProcessHeader.Where(a => a.ModelId == ModelId && a.Id == 0).FirstOrDefault();
            
            return View(data);
        }

        [AuthorizedAction]
        public IActionResult Mixings(string ModelId, int Id)
        {
            ViewBag.ModelId = ModelId;
            ViewBag.Product = GetBOMName(ModelId);
            var data = new ModelMixing();
            data.master = _context.ModelMaster.Where(a => a.ModelId == ModelId).FirstOrDefault();
            List<Manufacturing.Data.Entities.ModelRateType> RateList = _context.ModelRateType.Where(a => a.Active == true).ToList();
            ViewBag.RateList = new SelectList(RateList, "RateTypeCode", "RateTypeName");
            data.fOHBreakdown = _context.ModelDetailFOHBreakdown.Where(a => a.ModelId == ModelId && a.SPID == "SUB-00002").OrderByDescending(a=>a.Id).FirstOrDefault();
            data.detailProcessHeader = _context.ModelDetailProcessHeader.Where(a => a.ModelId == ModelId && a.Id == Id).FirstOrDefault();
            data.listDetailProcess = _context.ModelDetailProcess.Where(a => a.Active == true && a.ModelId == ModelId && a.ProcessHeaderNo == Id).ToList();
            data.listTableFOH = (from foh in _context.ModelDetailFOHBreakdown
                                 join machine in _context.ModelMachineMaster
                                 on foh.SPMachineID equals machine.MachineNo into machinefoh
                                 from machine in machinefoh.DefaultIfEmpty()
                                 where foh.SPID == "SUB-00002" && foh.ModelId == ModelId
                                 select new TableFOHViewModel
                                 {
                                     ModelId = foh.ModelId,
                                     FOHType = foh.FOHType,
                                     SubProcessSize = foh.SubProcessSize,
                                     OperationName = foh.OperationName,
                                     SPMachineID = foh.SPMachineID,
                                     SPSpeed = foh.SPSpeed,
                                     SPDuration = foh.SPDuration,
                                     SPQuantity = foh.SPQuantity,
                                     MachineName = machine.MachineName,
                                     PowerConsumption = foh.SPMachineID == null ? 0 : machine.PowerConsumption,
                                     FOHAmount = foh.SPMachineID == null ? foh.SPQuantity : machine.PowerConsumption * foh.SPQuantity * foh.SPDuration
                                 }).OrderBy(a => a.FOHType).ToList();
            return View(data);
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
            List<Manufacturing.Data.Entities.Items> MMEA = _context.Items.Where(a => a.RowStatus == 0 && a.InventoryPostingGroup == "FG").ToList();
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
                    List<Manufacturing.Data.Entities.Items> MMEA = _context.Items.Where(a => a.RowStatus == 0 && a.InventoryPostingGroup == "FG").ToList();
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
                    //currentData.SPMixerCapacity = model.SPMixerCapacity;
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

        [AuthorizedAPI]
        [HttpPatch]
        public JsonResult FOHDetaill(string FOHNo)
        {
            var result = "";
            if(FOHNo =="" || FOHNo == null)
            {
                result = "Gagal Mendapatkan Data";
            }
            else
            {
                var hapus = _context.ModelDetailFOHBreakdown.Where(a => a.ModelDetailFOHNo == FOHNo).SingleOrDefault();
                if(hapus == null)
                {
                    result = "FOH dengan nomor " + FOHNo + " tidak dapat ditemukan";
                }
                else
                {
                    hapus.Active = false;
                    hapus.LastModifiedAt = DateTime.Now;
                    hapus.LastModifiedBy = HttpContext.Session.GetString("EMailAddress");
                    try
                    {
                        _context.ModelDetailFOHBreakdown.Update(hapus);
                        _context.SaveChanges();
                        result = "sukses";
                    }catch(Exception ex)
                    {
                        result = "Terjadi kesalahan saat menghapus data";
                        throw;
                    }
                }
            }
            return Json(result);
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
                var data = _context.ModelDetailFOHBreakdown.Where(a => a.ModelId == ModelId).FirstOrDefault();
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









        /*Rate */
        [AuthorizedAPI]
        [HttpPost]
        public JsonResult RatePOST(ModelRateMaster model)
        {
            var result = "";
            if (model == null)
            {
                result = "Gagal Mendapatkan Data";
            }
            else
            {
                model.RateNo = GenerateRateNo();
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = HttpContext.Session.GetString("EMailAddress");
                try
                {
                    _context.ModelRateMaster.Add(model);
                    _context.SaveChanges();
                    result = "sukses";
                }
                catch (Exception ex)
                {
                    result = "Gagal saat menyimpan data " + ex;
                }
            }
            return Json(result);
        }

        [AuthorizedAPI]
        [HttpPut]
        public JsonResult RatesUpdate(ModelRateMaster model)
        {
            var result = "";
            if (model == null)
            {
                result = "Gagal Mendapatkan Data";
            }
            else
            {
                var current = _context.ModelRateMaster.Where(a => a.RateNo == model.RateNo).SingleOrDefault();
                if (current == null)
                {
                    result = "Rates dengan No " + model.RateNo + " Tidak ditemukan !!";
                }
                else
                {
                    current.RateName = model.RateName;
                    current.RegularRate = model.RegularRate;
                    current.Price = model.Price;
                    current.LemburRate = model.LemburRate;
                    current.WeekendRate = model.WeekendRate;
                    current.SetupPrice = model.SetupPrice;
                    current.PeakHourRate = model.PeakHourRate;
                    current.Unit = model.Unit;
                    current.MaintenancePrice = model.MaintenancePrice;
                    current.Capacity = model.Capacity;
                    current.AgeUsedMonth = model.AgeUsedMonth;
                    current.SalvageValue = model.SalvageValue;
                    current.MOQ = model.MOQ;
                    current.Tax = model.Tax;
                    current.Insurance = model.Insurance;

                    current.LastModifiedAt = DateTime.Now;
                    current.LastModifiedBy = HttpContext.Session.GetString("EMailAddress");

                    try
                    {
                        _context.ModelRateMaster.Update(current)
                            .Property(a => a.Id).IsModified = false;
                        _context.SaveChanges();
                        result = "sukses";
                    }
                    catch (Exception ex)
                    {
                        result = "Gagal memperbarui data dengan error " + ex;
                        throw;
                    }
                }

            }
            return Json(result);
        }

        /*Labour*/
        [AuthorizedAction]
        public IActionResult RateLabour()
        {
            var data = new ModelRateViewModel();
            data.lisRateMaster = _context.ModelRateMaster.Where(a => a.RateType == "Labour" && a.Active == true).ToList();
            return View(data);
        }


        /*Utility*/
        [AuthorizedAction]
        public IActionResult RateUtility()
        {
            var data = new ModelRateViewModel();
            data.lisRateMaster = _context.ModelRateMaster.Where(a => a.RateType == "Utility" && a.Active == true).ToList();
            return View(data);
        }


        /*Tangki*/
        [AuthorizedAction]
        public IActionResult RateTangki()
        {
            var data = new ModelRateViewModel();
            data.lisRateMaster = _context.ModelRateMaster.Where(a => a.RateType == "Tangki" && a.Active == true).ToList();
            return View(data);
        }


        //Consumable
        [AuthorizedAction]
        public IActionResult Consumables()
        {
            var data = new ModelRateViewModel();
            data.lisRateMaster = _context.ModelRateMaster.Where(a => a.RateType == "Consumables" && a.Active == true).ToList();
            List<Manufacturing.Data.Entities.UnitOfMeasures> ListUnit = _context.UnitOfMeasures.Where(a => a.RowStatus == 0).OrderByDescending(a=>a.DefaultUnitOfMeasure).ToList();
            ViewBag.UnitList = new SelectList(ListUnit, "UOMCode", "UOMDescription");
            return View(data);
        }


        //Raw & Support Mats
        [AuthorizedAction]
        public IActionResult RateRawSupport()
        {
            var data = new ModelRateViewModel();
            data.lisRateMaster = _context.ModelRateMaster.Where(a => a.RateType == "RawNSupp" && a.Active == true).ToList();
            List<Manufacturing.Data.Entities.UnitOfMeasures> ListUnit = _context.UnitOfMeasures.Where(a => a.RowStatus == 0).OrderByDescending(a => a.DefaultUnitOfMeasure).ToList();
            ViewBag.UnitList = new SelectList(ListUnit, "UOMCode", "UOMDescription");
            return View(data);
        }

        //Cukai
        [AuthorizedAction]
        public IActionResult RateCukai()
        {
            var data = new ModelRateViewModel();
            data.lisRateMaster = _context.ModelRateMaster.Where(a => a.RateType == "Cukai" && a.Active == true).ToList();
            List<Manufacturing.Data.Entities.UnitOfMeasures> ListUnit = _context.UnitOfMeasures.Where(a => a.RowStatus == 0).OrderByDescending(a => a.DefaultUnitOfMeasure).ToList();
            ViewBag.UnitList = new SelectList(ListUnit, "UOMCode", "UOMDescription");
            return View(data);
        }

        //Packaging Material
        [AuthorizedAction]
        public IActionResult RatesPackaging()
        {
            var data = new ModelRateViewModel();
            data.lisRateMaster = _context.ModelRateMaster.Where(a => a.RateType == "Packaging" && a.Active == true).ToList();
            List<Manufacturing.Data.Entities.UnitOfMeasures> ListUnit = _context.UnitOfMeasures.Where(a => a.RowStatus == 0).OrderByDescending(a => a.DefaultUnitOfMeasure).ToList();
            ViewBag.UnitList = new SelectList(ListUnit, "UOMCode", "UOMDescription");
            return View(data);
        }

        //Gedung dan Kendaraan
        [AuthorizedAction]
        public IActionResult RatesGedungKendaraan()
        {
            var data = new ModelRateViewModel();
            data.lisRateMaster = _context.ModelRateMaster.Where(a => a.RateType == "GnK" && a.Active == true).ToList();
            return View(data);
        }

        //ProductionCapacity
        [AuthorizedAction]
        public IActionResult RateProductionCapacity()
        {
            var data = new ModelRateViewModel();
            data.lisRateMaster = _context.ModelRateMaster.Where(a => a.RateType == "PC" && a.Active == true).ToList();
            List<Manufacturing.Data.Entities.UnitOfMeasures> ListUnit = _context.UnitOfMeasures.Where(a => a.RowStatus == 0).OrderByDescending(a => a.DefaultUnitOfMeasure).ToList();
            ViewBag.UnitList = new SelectList(ListUnit, "UOMCode", "UOMDescription");
            return View(data);
        }










        /*untuk show rates sebelum di edit*/
        [AuthorizedAPI]
        [HttpGet]
        public JsonResult SingleRates(string No)
        {
            if(No =="" || No == null)
            {
                return Json(false);
            }
            else
            {
                var Rates = _context.ModelRateMaster.Where(a => a.RateNo == No).SingleOrDefault();
                if(Rates == null)
                {
                    return Json(false);
                }
                else
                {
                    return Json(Rates);
                }
            }
        }

        [AuthorizedAPI]
        [HttpPut]
        public JsonResult HideRates(string No)
        {
            var result = "";
            if(No =="" || No == null)
            {
                result = "Gagal mendapatkan Rates No";
            }
            else
            {
                var hide = _context.ModelRateMaster.Where(a => a.RateNo == No).SingleOrDefault();
                if(hide == null)
                {
                    result = "Rate dengan nomor "+No+" tidak ditemukan";
                }
                else
                {
                    hide.Active = false;
                    hide.LastModifiedAt = DateTime.Now;
                    hide.LastModifiedBy = HttpContext.Session.GetString("EMailAddress");
                    try
                    {
                        _context.ModelRateMaster.Update(hide).Property(a => a.Id).IsModified = false;
                        _context.SaveChanges();
                        result = "sukses";
                    }
                    catch(Exception ex)
                    {
                        result = "Gagal menghapus data";
                        throw;
                    }
                }
            }
            return Json(result);
        }

        //Cek Model di DB
        [HttpGet]
        public JsonResult CekModelDetail(string Id)
        {
            var data = _context.ModelDetailProcessHeader.Where(a => a.ModelId == Id).ToList();
            return Json(data);
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

        public string GenerateRateNo()
        {
            string Id = "RT-00001";
            var MaxId = _context.ModelRateMaster.OrderByDescending(a => a.Id).Select(a => a.RateNo).FirstOrDefault();
            if (MaxId != null)
            {
                char[] trimmed = { 'R', 'T', '-' };
                int currentIds = Convert.ToInt32(MaxId.Trim(trimmed));
                if (currentIds < 10)
                {
                    Id = "RT-0000" + (currentIds + 1);
                }
                else if (currentIds < 100)
                {
                    Id = "RT-000" + (currentIds + 1);
                }
                else if (currentIds < 1000)
                {
                    Id = "RT-00" + (currentIds + 1);
                }
                else if (currentIds < 10000)
                {
                    Id = "RT-0" + (currentIds + 1);
                }
                else
                {
                    Id = "RT-" + (currentIds + 1);
                }
            }
            return Id;
        }

        public string GenerateWIPHeaderID()
        {
            string Id = "WIP-00001";
            var MaxId = _context.ModelWIPProcessHeader.OrderByDescending(a => a.Id).Select(a => a.ModelHeaderId).FirstOrDefault();
            if (MaxId != null)
            {
                char[] trimmed = { 'W', 'I', 'P', '-' };
                int currentIds = Convert.ToInt32(MaxId.Trim(trimmed));
                if (currentIds < 10)
                {
                    Id = "WIP-0000" + (currentIds + 1);
                }
                else if (currentIds < 100)
                {
                    Id = "WIP-000" + (currentIds + 1);
                }
                else if (currentIds < 1000)
                {
                    Id = "WIP-00" + (currentIds + 1);
                }
                else if (currentIds < 10000)
                {
                    Id = "WIP-0" + (currentIds + 1);
                }
                else
                {
                    Id = "WIP-" + (currentIds + 1);
                }
            }
            return Id;
        }
    }
}