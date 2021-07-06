using Microsoft.AspNetCore.Mvc;
using Manufacturing.Data;
using Manufacturing.Helpers;
using System.Linq;
using Manufacturing.Data.Entities;
using System;
using Microsoft.AspNetCore.Http;

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
            if(No == "Item")
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
            if(ModelState.IsValid) {
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
                catch(Exception ex)
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
            if(model == null)
            {
                result = "Gagal Mendapatkan Data";
            }
            else
            {
                //get current val
                var current = _context.ModelWIPProcessLine.Where(a => a.ModelWIPLineId == model.ModelWIPLineId).SingleOrDefault();
                if(current == null)
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
                    }catch(Exception ex)
                    {
                        result = "Gagal Menyimpan Data !";
                    }
                }
            }
            return Json(result);
        }



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
