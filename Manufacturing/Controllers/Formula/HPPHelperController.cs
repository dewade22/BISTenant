using Microsoft.AspNetCore.Mvc;
using Manufacturing.Data;
using Manufacturing.Models;
using Manufacturing.Helpers;
using System.Linq;
using Manufacturing.Data.Entities;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Manufacturing.Controllers.Formula
{
    public class HPPHelperController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HPPHelperController(ApplicationDbContext context)
        {
            _context = context;
        }

        public JsonResult Items(string No)
        {
            var res = _context.Items.Where(a => a.ItemNo == No).FirstOrDefault();
            return Json(res);
        }

        public JsonResult SelectItem(string No)
        {
            if (No == "Item")
            {
                var data = (from item in _context.Items
                            where item.RowStatus == 0
                            select new Models.SelectListModel
                            {
                                ValueCode = item.ItemNo,
                                ValueName = item.Description
                            }).ToList();
                return Json(data);
            }
            else if(No == "PraMixing")
            {
                var data = (from pramixing in _context.ModelWIPOutput
                            select new Models.SelectListModel
                            {
                                ValueCode = pramixing.ItemNo,
                                ValueName = pramixing.Description
                            }).ToList();
                return Json(data);
            }
            else
            {
                var data = (from RawSupp in _context.ModelRateMaster
                            where RawSupp.Active == true && RawSupp.RateType == No
                            select new Models.SelectListModel
                            {
                                ValueCode = RawSupp.RateNo,
                                ValueName = RawSupp.RateName
                            }).ToList();
                return Json(data);
            }
        }






        /*CRUD Function*/

        [AuthorizedAPI]
        [HttpPost]
        public JsonResult SavePramixing(ModelWIPProcessLine model)
        {
            var result = "Data Tidak Dapat Ditambahkan";
            if (ModelState.IsValid) {
                model.ModelWIPLineId = WIPLineId();
                model.ItemPrice = ItemPriceWIPLine(model);
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = HttpContext.Session.GetString("EMailAddress");
                try
                {
                    _context.ModelWIPProcessLine.Add(model);
                    _context.SaveChanges();
                    result = "sukses";
                }
                catch (Exception ex)
                {
                    result = "Gagal Menambahkan Data";
                }
            }
            return Json(result);
        }

        [AuthorizedAPI]
        [HttpGet]
        public JsonResult GetPraMixingLine(string No)
        {
            var data = _context.ModelWIPProcessLine.Where(a => a.ModelWIPLineId == No).FirstOrDefault();
            return Json(data);
        }

        [AuthorizedAPI]
        [HttpPut]
        public JsonResult UpdatePramixing(ModelWIPProcessLine model)
        {
            var result = "";
            if (model == null)
            {
                result = "Gagal Mendapatkan Data";
            }
            else
            {
                //get current val
                var current = _context.ModelWIPProcessLine.Where(a => a.ModelWIPLineId == model.ModelWIPLineId).SingleOrDefault();
                if (current == null)
                {
                    result = "Data dengan Id = " + model.ModelWIPLineId + " Tidak ditemukan";
                }
                else
                {
                    current.ItemType = model.ItemType;
                    current.ItemNo = model.ItemNo;
                    current.ItemName = model.ItemName;
                    current.ItemQty = model.ItemQty;
                    current.ItemUnit = model.ItemUnit;
                    current.ItemPrice = ItemPriceWIPLine(model);
                    current.ProcessHour = model.ProcessHour;
                    current.LastModifiedAt = DateTime.Now;
                    current.lastModifiedBy = HttpContext.Session.GetString("EMailAddress");
                    try
                    {
                        _context.ModelWIPProcessLine.Update(current).Property(a => a.Id).IsModified = false; ;
                        _context.SaveChanges();
                        result = "sukses";
                    } catch (Exception ex)
                    {
                        result = "Gagal Menyimpan Data !";
                    }
                }
            }
            return Json(result);
        }

        [AuthorizedAPI]
        [HttpPut]
        public JsonResult DisposePramixingLine(string No)
        {
            var result = "";
            if (No == null || No == "")
            {
                result = "Couldn't Get Id From Input";
            }
            else
            {
                var data = _context.ModelWIPProcessLine.Where(a => a.ModelWIPLineId == No).SingleOrDefault();
                if (data == null)
                {
                    result = "Item With Id = " + No + " Not Found!";
                }
                else
                {
                    data.Active = false;
                    data.LastModifiedAt = DateTime.Now;
                    data.lastModifiedBy = HttpContext.Session.GetString("EMailAddress");
                    try
                    {
                        _context.ModelWIPProcessLine.Update(data).Property(a => a.Id).IsModified = false;
                        _context.SaveChanges();
                        result = "sukses";
                    } catch (Exception e)
                    {
                        result = "Gagal menghapus data " + e;
                    }
                }
            }
            return Json(result);
        }

        [AuthorizedAPI]
        [HttpGet]
        public JsonResult CalculateWIPLine(string No)
        {
            if (No == null || No == "")
            {
                return Json(0);
            }
            else
            {
                var data = _context.ModelWIPProcessLine.Where(a => a.ModelWIPHeaderId == No && a.Active == true).ToList();
                var batc = _context.ModelWIPProcessHeader.Where(a => a.ModelHeaderId == No).SingleOrDefault();
                if (data == null)
                {
                    return Json(0);
                }
                else
                {
                    var hasil = (from d in data
                                 group d by new { d.ModelWIPHeaderId }
                                 into res
                                 select new ModelCalculatePraMixing {
                                     BatchCost = (res.Sum(a => a.ItemType == "Labour" ? a.ItemQty * a.ItemPrice * a.ProcessHour : a.ItemQty * a.ItemPrice)),
                                     UnitCost = (res.Sum(a => a.ItemType == "Labour" ? (a.ItemQty * a.ItemPrice * a.ProcessHour) / batc.QtyOutput : (a.ItemQty * a.ItemPrice) / batc.QtyOutput))
                                 });
                    return Json(hasil);
                }
            }
        }

        [AuthorizedAPI]
        [HttpPost]
        public JsonResult SaveWIPLineCost(ModelCalculatePraMixing model)
        {
            var result = "";
            if (model == null)
            {
                result = "Couldn't get calculation result !";
            }
            else
            {
                var getItemNo = _context.ModelWIPProcessHeader.Where(a => a.ModelHeaderId == model.WIPHeaderId).FirstOrDefault();
                if (getItemNo == null)
                {
                    result = "Couldn't get Process Id, Please Reload Page";
                }
                else
                {
                    var baseItem = _context.Items.Where(a => a.ItemNo == getItemNo.ItemOutputId).FirstOrDefault();
                    var newItem = _context.ModelWIPOutput.Where(a => a.ItemNo == baseItem.ItemNo).FirstOrDefault();

                    //Cek apakah sudah tersimpan di DB baru

                    if (newItem == null)
                    {
                        //insert
                        newItem = new ModelWIPOutput();
                        newItem.Description = baseItem.Description;
                        newItem.BaseUnitOfMeasure = baseItem.BaseUnitofMeasure;
                        newItem.InventoryPostingGroup = baseItem.InventoryPostingGroup;
                        newItem.ItemNo = baseItem.ItemNo;
                        newItem.ItemCost = model.UnitCost;
                        newItem.ItemQty = getItemNo.QtyOutput;
                        newItem.CreatedAt = DateTime.Now;
                        newItem.CreatedBy = HttpContext.Session.GetString("EMailAddress");
                        try
                        {
                            _context.ModelWIPOutput.Add(newItem);
                            _context.SaveChanges();
                            result = "sukses";
                        } catch (Exception e)
                        {
                            result = "Couldn't Add New Record";
                            throw;
                        }
                    }
                    else
                    {
                        //Update
                        newItem.Description = baseItem.Description;
                        newItem.BaseUnitOfMeasure = baseItem.BaseUnitofMeasure;
                        newItem.InventoryPostingGroup = baseItem.InventoryPostingGroup;
                        newItem.LastItemCost = model.UnitCost;
                        newItem.ItemQty = getItemNo.QtyOutput;
                        newItem.LastModifiedAt = DateTime.Now;
                        newItem.LastModifiedBy = HttpContext.Session.GetString("EMailAddress");
                        try
                        {
                            _context.ModelWIPOutput.Update(newItem).Property(a => a.ItemId).IsModified = false;
                            _context.SaveChanges();
                            result = "sukses";
                        }
                        catch (Exception e)
                        {
                            result = "Couldn't Update The Record.! ";
                            throw;
                        }
                    }

                }
            }
            return Json(result);
        }

        [AuthorizedAPI]
        [HttpPost]
        public JsonResult SaveDetailProcessHeader(ModelDetailProcessHeader model)
        {
            var result = "";
            var Id = 0;
            if (model != null)
            {
                try
                {
                    _context.ModelDetailProcessHeader.Add(model);
                    _context.SaveChanges();
                    result = "sukses";
                    Id = model.Id;
                }
                catch (Exception e)
                {
                    result = "Terjadi kesalahan saat menyimpan data " + e;
                }
            }
            else
            {
                result = "Terjadi kesalahan saat mencari model";
            }
            return new JsonResult(new { result = result, Id = Id });
        }

        [AuthorizedAPI]
        [HttpPost]
        public JsonResult SyncronFOH(string Model, int Header, string SPID)
        {
            var result = "";
            var type = "electricity";
            var cek = _context.ModelDetailProcess.Where(a => a.ModelId == Model && a.SubProcessId == SPID && a.Type == "electricity" && a.ProcessHeaderNo == Header).FirstOrDefault();
            if (cek == null)
            {
                //Add
                var sukses = AddSyncronFOH(Model, SPID, Header, type);
                if(sukses == true)
                {
                    result = "sukses";
                }
                else
                {
                    result = "Terjadi kesalahan saat menambahkan data";
                }
            }
            else
            {
                //Hapus kemudian tambahkan Ulang
                //hapus
                var suksesHapus = RemoveSyncFOH(Model, SPID);
                if(suksesHapus == true)
                {
                    var suksesTambah = AddSyncronFOH(Model, SPID, Header, type);
                    if(suksesTambah == true)
                    {
                        result = "sukses";
                    }
                    else
                    {
                        result = "Terjadi kesalahan saat sinkronisasi data";
                    }
                }
                else
                {
                    result = "Terjadi kesalahan saat sinkronisasi data";
                }

            }
            return Json(result);
        }




        //CRUD Helper Class
        public decimal ItemPriceWIPLine(ModelWIPProcessLine model)
        {
            decimal price;
            if (model.ItemType == "Item")
            {
                price = (decimal)_context.Items.Where(a => a.ItemNo == model.ItemNo).Select(a => a.LastDirectCost).FirstOrDefault();
            }
            else
            {
                var getFOHParam = _context.ModelRateMaster.Where(a => a.RateNo == model.ItemNo).FirstOrDefault();
                if (getFOHParam.AgeUsedMonth != 0)
                {
                    var baseItemPrice = getFOHParam.RegularRate;
                    if (baseItemPrice == 0)
                    {
                        baseItemPrice = getFOHParam.Price;
                    }
                    price = (decimal)(Math.Ceiling((decimal)(model.ProcessHour / 24)) / 30 * (baseItemPrice / getFOHParam.AgeUsedMonth));
                }
                else
                {
                    price = (decimal)_context.ModelRateMaster.Where(a => a.RateNo == model.ItemNo).Select(a => a.RegularRate).FirstOrDefault();
                    if (price == 0)
                    {
                        price = (decimal)_context.ModelRateMaster.Where(a => a.RateNo == model.ItemNo).Select(a => a.Price).FirstOrDefault();
                    }
                }
            }
            return price;
        }

        public Boolean AddSyncronFOH(string Model, string SPID, int Header, string type)
        {
            //Ambil SubProcess Size untuk dicompare dengan mixing Size
            var size = _context.ModelDetailFOHBreakdown.Where(a => a.ModelId == Model && a.SPID == SPID).FirstOrDefault();
            var fohCapacity = size.SubProcessSize;
            var header = _context.ModelDetailProcessHeader.Where(a => a.Id == Header).FirstOrDefault();
            var mixingSize = header.ProcessSize;
            decimal pengali = Math.Ceiling(mixingSize / fohCapacity);


            //ambil data yang mau di add
            List<Manufacturing.Data.Entities.ModelDetailFOHBreakdown> data = _context.ModelDetailFOHBreakdown.Where(a => a.ModelId == Model && a.SPID == SPID && a.FOHType == "electricity" && a.Active == true).ToList();
            var modelBaru = new ModelDetailProcess();
            foreach (ModelDetailFOHBreakdown value in data)
            {
                modelBaru.Id = 0;
                modelBaru.ProcessHeaderNo = Header;
                modelBaru.ModelId = Model;
                modelBaru.SubProcessId = SPID;
                modelBaru.Type = type;
                modelBaru.ItemNo = value.SPMachineID;
                modelBaru.ItemDescription = MachineName(value.SPMachineID);
                modelBaru.Description = value.OperationName;
                modelBaru.ItemQty = value.SPQuantity;
                modelBaru.ItemCost = pengali * ItemCostForFOH("electricity", value.SPMachineID, value.SPDuration);
                modelBaru.ProcessHour = pengali * value.SPDuration;
                modelBaru.CreatedAt = DateTime.Now;
                modelBaru.CreatedBy = HttpContext.Session.GetString("EMailAddress");
                try
                {
                    _context.ModelDetailProcess.Add(modelBaru);
                    _context.SaveChanges();
                    modelBaru.Id += 1;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
            return true;
        }

        public Boolean RemoveSyncFOH(string Model, string SPID)
        {
            var remove = _context.ModelDetailProcess.Where(a => a.Type == "electricity" && a.ModelId == Model && a.SubProcessId == SPID);
            foreach (var item in remove)
            {
                try
                {
                    _context.ModelDetailProcess.Remove(item);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }

        public string MachineName(string ItemNo)
        {
            return _context.ModelMachineMaster.Where(a => a.MachineNo == ItemNo).Select(a => a.MachineName).SingleOrDefault();
        }

        public decimal ItemCostForFOH(string Type, string ItemNo, decimal? ProcessHour)
        {
            decimal price = 0;
            if (Type == "electricity")
            {
                var getFOHParam = _context.ModelMachineMaster.Where(a => a.MachineNo == ItemNo).FirstOrDefault();
                if (getFOHParam.MaximumAgeUse != 0)
                {
                    //machine maintenance price belum masuk
                    var baseItemPrice = getFOHParam.MachinePrice;
                    price = (decimal)(ProcessHour / 24) / 30 * (baseItemPrice / (getFOHParam.MaximumAgeUse*12));
                }
                else
                {
                    price = (decimal)_context.ModelMachineMaster.Where(a => a.MachineNo == ItemNo).Select(a => a.MachinePrice).FirstOrDefault();
                }
            }

            return price;
        }

        
        



        //ID Auto Generate
        public string WIPLineId()
        {
            string ID = "WIL-00001";
            var MaxId = _context.ModelWIPProcessLine.OrderByDescending(a => a.Id).Select(a => a.ModelWIPLineId).FirstOrDefault();
            if (MaxId != null)
            {
                char[] trimmed = { 'W', 'I', 'L', '-' };
                int currentIds = Convert.ToInt32(MaxId.Trim(trimmed));
                if (currentIds + 1 < 10)
                {
                    ID = "WIL-0000" + (currentIds + 1);
                }
                else if (currentIds + 1 < 100)
                {
                    ID = "WIL-000" + (currentIds + 1);
                }
                else if (currentIds + 1 < 1000)
                {
                    ID = "WIL-00" + (currentIds + 1);
                }
                else if (currentIds + 1 < 10000)
                {
                    ID = "WIL-0" + (currentIds + 1);
                }
                else
                {
                    ID = "WIL-" + (currentIds + 1);
                }
            }
            return (ID);
        }
    }
}
