using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manufacturing.Helpers;
using Manufacturing.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Manufacturing.Data.Entities;
using System.Data;

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
            
            ViewBag.Category = category; 
            return View();
        }

        [AuthorizedAction]
        public IActionResult TodaySalesBIP(DateTime? dateTime, string category)
        {

            ViewBag.Category = category;
            return View();
        }

        //[AuthorizedAPI]
        public IActionResult TodaySales(DateTime? dateTime, string category)
        {
            var transaksi = TransactionDetail(dateTime, category);
            var DetilTransaksi = (from trans in transaksi
                                  group trans by new { trans.SONumber, trans.SalesPerson, trans.BilltoName, trans.DocumentNo, trans.Category } into hasil
                                  select new spSalesInvoiceSummaryPivotModel
                                  {
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
            return new JsonResult(DetilTransaksi);
        }

        public IEnumerable<spSalesInvoiceSummaryPivotModel>TransactionDetail(DateTime? dateTime, string category)
        {
            List<spSalesInvoiceSummaryPivotModel> DetilTransaksi = new List<spSalesInvoiceSummaryPivotModel>();
            var parameters = new[]
            {
                new SqlParameter("@StartPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = dateTime.Value.Date},
                new SqlParameter("@EndPeriod", System.Data.SqlDbType.Date) { Direction = ParameterDirection.Input, Value = dateTime.Value.Date},
                new SqlParameter("@prmCategory", System.Data.SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = category}
            };
            try {
                DetilTransaksi = _context.SpSalesInvoiceSummaryPivotModels.FromSqlRaw(@"exec spSalesInvoiceSummaryPivot @StartPeriod, @EndPeriod, @prmCategory", parameters).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            //Filter Lagi Berdasarkan SO
            
            return DetilTransaksi;
        }
    }
}
