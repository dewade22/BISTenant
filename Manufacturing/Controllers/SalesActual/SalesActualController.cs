using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Manufacturing.Helpers;
using Manufacturing.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Manufacturing.Data.Entities;
using System.Data;
using Manufacturing.Models.DataTable;
using Manufacturing.Services;

namespace Manufacturing.Controllers
{
    public class SalesActualController : Controller
    {
        private ApplicationDbContext _context;

        public SalesActualController( ApplicationDbContext context)
        {
            _context = context;
        }

        [AuthorizedAction]
        public IActionResult BMI()
        {
            return View();
        }

        [AuthorizedAction]
        public IActionResult BIP()
        {
            return View();
        }

        [AuthorizedAction]
        public IActionResult SalesBoardBMI()
        {
            return View();
        }

        [AuthorizedAction]
        public IActionResult SalesBoardBIP()
        {
            return View();
        }
       

        //untuk Form SalesActual dan LandedCost BMI BIP
        [AuthorizedAPI]
        public IActionResult SalesAndTarget(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            var getSP = TransactionSalesActual(dateTime);
            var landCost = TransactionLandedCost(dateTime);


            return new JsonResult(new { SalesActual = getSP, LandedCost = landCost });
        }

        //untuk Sales Actual
        public IEnumerable<spRptSalesActualModel>TransactionSalesActual(DateTime? dateTime)
        {
            List<spRptSalesActualModel>salesActual = new List<spRptSalesActualModel>();
            try
            {
                salesActual =  _context.SpRptSalesActualModels.FromSqlRaw(@"exec [dbo].[spRptSalesActual] {0}", dateTime.Value.Date).AsNoTracking().ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return (salesActual);
        }

        //Untuk LandedCost
        public IEnumerable<spRptSalesActual_LandedCostModel>TransactionLandedCost(DateTime? dateTime)
        {
            List<spRptSalesActual_LandedCostModel> LandedCost = new List<spRptSalesActual_LandedCostModel>();
            try {
                LandedCost = _context.SpRptSalesActual_LandedCosts.FromSqlRaw(@"exec [dbo].[spRptSalesActual_LandedCost] {0}", dateTime.Value.Date).AsNoTracking().ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return LandedCost;
        }

        [AuthorizedAction]
        public IActionResult TodaySalesBMI(DateTime? dateTime, string category)
        {
            if(category== null)
            {
                category = "All";
            }
            ViewBag.Category = category; 
            return View();
        }

        [AuthorizedAction]
        public IActionResult TodaySalesBIP(DateTime? dateTime, string category)
        {
            if (category == null)
            {
                category = "All";
            }
            ViewBag.Category = category;
            return View();
        }


        [AuthorizedAPI]
        public IActionResult TodaySalesDataBMI(DateTime? dateTime, string category)
        {
            if(category == null)
            {
                category = "";
            }
            var parameters = new[]
            {
                new SqlParameter("@StartPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = dateTime.Value.Date},
                new SqlParameter("@EndPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = dateTime.Value.Date},
                new SqlParameter("@prmCategory", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = category}
            };
            var transaksi = TransactionDetailBMI(parameters);
            var DetilTransaksi = FilterSO(transaksi);
            return new JsonResult(DetilTransaksi);
        }

        public IEnumerable<spSalesInvoiceSummaryPivotModel>TransactionDetailBMI(SqlParameter[] parameters)
        {
            List<spSalesInvoiceSummaryPivotModel> DetilTransaksi = new List<spSalesInvoiceSummaryPivotModel>();
            
            try {
                DetilTransaksi = _context.SpSalesInvoiceSummaryPivotModels.FromSqlRaw(@"exec spSalesInvoiceSummaryPivot @StartPeriod, @EndPeriod, @prmCategory", parameters).AsNoTracking().ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            //Filter Lagi Berdasarkan SO
            
            return DetilTransaksi;
        }


        [AuthorizedAPI]
        public IActionResult TodaySalesDataBIP(DateTime? dateTime, string category)
        {
            if (category == null)
            {
                category = "";
            }
            var parameters = new[]
            {
                new SqlParameter("@StartPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = dateTime.Value.Date},
                new SqlParameter("@EndPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = dateTime.Value.Date},
                new SqlParameter("@prmProductGroup", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = category}
            };
            var transaksi = TransactionDetailBIP(parameters);
            var DetilTransaksi = FilterSO(transaksi);

            return new JsonResult(DetilTransaksi);
        }

        public IEnumerable<spSalesInvoiceSummaryPivotModel> TransactionDetailBIP(SqlParameter[] parameters)
        {
            List<spSalesInvoiceSummaryPivotModel> DetilTransaksi = new List<spSalesInvoiceSummaryPivotModel>();

            try
            {
                DetilTransaksi = _context.SpSalesInvoiceSummaryPivotModels.FromSqlRaw(@"exec spSalesInvoiceSummaryPivot @StartPeriod, @EndPeriod, '', @prmProductGroup", parameters).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
            return DetilTransaksi;
        }



        [AuthorizedAction]
        public IActionResult MonthlySalesBMI(DateTime? dateTime, string category)
        {
            if (category == null)
            {
                category = "All";
            }

            ViewBag.Category = category;
            return View();
        }

        [AuthorizedAction]
        public IActionResult MonthlySalesBIP(DateTime? dateTime, string category)
        {
            if (category == null)
            {
                category = "All";
            }
            ViewBag.Category = category;
            return View();
        }

        [AuthorizedAPI]
        public IActionResult MonthlySalesDataBMI(DateTime? dateTime, string category)
        {
            if(category == null)
            {
                category = "";
            }
            var startDate = new DateTime(dateTime.Value.Year, dateTime.Value.Month, 1);
            var parameters = new[]
            {
                new SqlParameter("@StartPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = startDate},
                new SqlParameter("@EndPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = dateTime.Value.Date},
                new SqlParameter("@prmCategory", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = category}
            };
            var transaksi = TransactionDetailBMI(parameters);
            var filtered = FilterSO(transaksi);

            return new JsonResult(filtered);
        }

        [AuthorizedAPI]
        public IActionResult MonthlySalesDataBIP(DateTime? dateTime, string category)
        {
            if (category == null)
            {
                category = "";
            }
            var startDate = new DateTime(dateTime.Value.Year, dateTime.Value.Month, 1);
            var parameters = new[]
            {
                new SqlParameter("@StartPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = startDate},
                new SqlParameter("@EndPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = dateTime.Value.Date},
                new SqlParameter("@prmProductGroup", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = category}
            };
            var transaksi = TransactionDetailBIP(parameters);
            var filtered = FilterSO(transaksi);
            return new JsonResult(filtered);
        }


        [AuthorizedAction]
        public IActionResult YearlySalesBMI(DateTime? dateTime, string category)
        {
            var cat = category;
            if (category == null)
            {
                category = "All";
                cat = "";
            }
            ViewBag.Category = category;

            //untuk client side
            var startDate = new DateTime(dateTime.Value.Year, 1, 1);
            var parameters = new[]
            {
                new SqlParameter("@StartPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = startDate},
                new SqlParameter("@EndPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = dateTime.Value.Date},
                new SqlParameter("@prmCategory", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = cat}
            };
            var transaksi = TransactionDetailBMI(parameters);
            var filtered = FilterSO(transaksi);

            return View(filtered);
        }

        [AuthorizedAPI]
        [HttpPost]
        public IActionResult YearlySalesDataBMI([FromBody] DtParameters dtParameters, DateTime? dateTime, string category)
        {
           if (category == null)
            {
                category = "";
            }
            var startDate = new DateTime(dateTime.Value.Year, 1, 1);
            var parameters = new[]
            {
                new SqlParameter("@StartPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = startDate},
                new SqlParameter("@EndPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = dateTime.Value.Date},
                new SqlParameter("@prmCategory", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = category}
            };
            var transaksi = TransactionDetailBMI(parameters);
            var filtered = FilterSO(transaksi);
            var result = filtered.AsQueryable();
            var filters = filtered;
            var searchBy = dtParameters.Search?.Value;

            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.SalesPerson != null && r.SalesPerson.ToLower().Contains(searchBy.ToLower()) ||
                r.DocumentNo != null && r.DocumentNo.ToLower().Contains(searchBy.ToLower()) ||
                r.SONumber != null && r.SONumber.ToLower().Contains(searchBy.ToLower()) ||
                r.BilltoName != null && r.BilltoName.ToLower().Contains(searchBy.ToLower()) ||
                r.Category != null && r.Category.ToLower().Contains(searchBy.ToLower())
                );
            }
            result =orderAscendingDirection ? result.OrderByDynamic(orderCriteria, (LinqExtension.Order)DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria, (LinqExtension.Order)DtOrderDir.Desc);
            var filteredResultsCount = result.Count();
            var totalResultsCount = filters.Count();

            return Json(new DtResult<spSalesInvoiceSummaryPivotModel>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
            //return new JsonResult(filtered);
        }

        //Filter Berdasarkan SO
        public IEnumerable<spSalesInvoiceSummaryPivotModel> FilterSO(IEnumerable<spSalesInvoiceSummaryPivotModel> model)
        {
            var DetilTransaksi = (from trans in model
                                  group trans by new { trans.SONumber, trans.SalesPerson, trans.BilltoName, trans.DocumentNo, trans.Category, trans.DocumentDate } into hasil
                                  select new spSalesInvoiceSummaryPivotModel
                                  {
                                      DocumentDate = hasil.Key.DocumentDate,
                                      SONumber = hasil.Key.SONumber,
                                      SalesPerson = hasil.Key.SalesPerson,
                                      BilltoName = hasil.Key.BilltoName,
                                      DocumentNo = hasil.Key.DocumentNo,
                                      Qty = hasil.Sum(a => a.Qty),
                                      Liters = hasil.Sum(a => a.Liters),
                                      Cost = hasil.Sum(a => a.Cost),
                                      Amount = hasil.Sum(a => a.Amount),
                                      Discount = hasil.Sum(a => a.Discount),
                                      Tax = hasil.Sum(a => a.Tax),
                                      AmountIncdTax = hasil.Sum(a => a.AmountIncdTax),
                                      LandedCost = hasil.Sum(a => a.LandedCost),
                                      Revenue = hasil.Sum(a => a.Revenue),
                                      Category = hasil.Key.Category,
                                      Liters_Sub = hasil.Sum(a => a.Liters_Sub),
                                      AmountIncdTax_Sub = hasil.Sum(a => a.AmountIncdTax_Sub)
                                  }).ToList();
            return DetilTransaksi;
        }

        [AuthorizedAPI]
        public IActionResult SBBMI(DateTime? dateTime)
        {
            if(dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            var getSalesBoard = SalesBoard(dateTime);
            var getTotalProduct = TotalProduct(getSalesBoard);
            var getTotalSales = TotalSalesPerson(getSalesBoard);
            return new JsonResult(new { hasil = getSalesBoard, product = getTotalProduct, sales = getTotalSales});
        }

        public IEnumerable<spRptSalesBoardModel> SalesBoard(DateTime? dateTime)
        {
            List<spRptSalesBoardModel> SalesBoards = new List<spRptSalesBoardModel>();
            try
            {
                SalesBoards = _context.SpRptSalesBoardModel.FromSqlRaw(@"exec [dbo].[spRptSalesBoard] {0}", dateTime.Value.Date).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (SalesBoards);
        }

        public int TotalProduct(IEnumerable<spRptSalesBoardModel> models)
        {
            var DataProduct = (from product in models
                               group product by new { product.ItemCategory } into hasil
                               select new spRptSalesBoardModel
                               {
                                   ItemCategory = hasil.Key.ItemCategory
                               }).ToList();
            var totalProduct = DataProduct.Count();
            return (totalProduct);
        }

        public int TotalSalesPerson(IEnumerable<spRptSalesBoardModel> models)
        {
            var DataPerson = (from product in models
                               group product by new { product.SalesPerson } into hasil
                               select new spRptSalesBoardModel
                               {
                                   ItemCategory = hasil.Key.SalesPerson
                               }).ToList();
            var totalSalesPerson = DataPerson.Count();
            return (totalSalesPerson);
        }

        [AuthorizedAPI]
        public IActionResult SBBIP(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            var getSalesBoard = SalesBoard(dateTime);
            var getTotalProduct = TotalProduct(getSalesBoard);
            var getTotalSales = TotalSalesPerson(getSalesBoard);
            return new JsonResult(new { hasil = getSalesBoard, product = getTotalProduct, sales = getTotalSales });
        }

        [AuthorizedAction]
        public IActionResult DetilSalesBoard(DateTime? dateTime, string sales)
        {
            if(dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            ViewBag.DateTime = dateTime.Value.Date;
            ViewBag.SalesPerson = sales;
            return View();
        }

        [AuthorizedAction]
        public IActionResult DetilSalesBoards(DateTime? dateTime, string sales)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            ViewBag.DateTime = dateTime.Value.Date;
            ViewBag.SalesPerson = sales;
            return View();
        }

        [AuthorizedAPI]
        public IActionResult SalesBoardPerSales(DateTime? dateTime, string sales)
        {
            if(dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            var salesBoards = SalesBoard(dateTime);
            var persales = filterPerSales(salesBoards, sales);
            return new JsonResult (new {hasil = persales});
        }

        public IEnumerable<spRptSalesBoardModel> filterPerSales(IEnumerable<spRptSalesBoardModel> model, string sales)
        {
            List<spRptSalesBoardModel> data = new List<spRptSalesBoardModel>();

            data = (from mod in model
                    where mod.SalesPerson == sales
                    select mod
                    ).ToList();
            return data;
        }
    }
}
