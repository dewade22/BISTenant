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
    }
}
