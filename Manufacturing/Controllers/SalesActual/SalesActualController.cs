using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manufacturing.Helpers;
using Manufacturing.Data;

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

        [AuthorizedAPI]
        public IActionResult BMIDaysMonth(DateTime? dateTime)
        {
            int noDaysMonth = getNoDaysInMonthBMI(dateTime);
            int DayinMonth = getTotalDaysInMonthBMI(dateTime);

            return new JsonResult(new { NoDaysMonth = noDaysMonth, TotalDayMonth = DayinMonth });
        }

        [AuthorizedAPI]
        public IActionResult BMISalesAndTarget(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }

            var SalesBudget = TodayTransactionMonth(dateTime);

            return new JsonResult( new{ SalesActual = SalesBudget});
        }

        public IActionResult TodayTransactionMonth(DateTime? dateTime)
        {
            //Kategori
            var kategori = from cat in _context.DimensionValue
                           where cat.DimensionCode.ToLower() == "category" && cat.RowStatus == 0
                           orderby cat.DimensionValueId
                           select new
                           {
                               category = cat.DimensionValueName
                           };
                

            //Item Budget Entry
            var budgetEntry = (from budget in _context.ItemBudgetEntry
                               where budget.Date.Value.Year == dateTime.Value.Year
                               group budget by new { category = budget.BudgetDimension2Code } into result
                               select new
                               {
                                   catrgory = result.Key.category,
                                   MonthlyLitersBudget = result.Sum(a => a.Date.Value.Month == dateTime.Value.Month ? a.Liters : 0),
                                   MonthlyRevenueBudget = result.Sum(a => a.Date.Value.Month == dateTime.Value.Month ? a.SalesAmount : 0),
                                   YearlyLitersBudget = result.Sum(a => a.Date.Value.Year == dateTime.Value.Year ? a.Liters : 0),
                                   YearlyRevenueBudget = result.Sum(a => a.Date.Value.Year == dateTime.Value.Year ? a.SalesAmount : 0)
                               });

            var Budget = (from kat in kategori
                          join budget in budgetEntry
                          on kat.category equals budget.catrgory into hasil
                          from budget in hasil.DefaultIfEmpty()
                          select new
                          {
                              catrgory = kat.category,
                              MonthlyLitersBudget = budget.MonthlyLitersBudget,
                              MonthlyRevenueBudget = budget.MonthlyRevenueBudget,
                              YearlyLitersBudget = budget.YearlyLitersBudget,
                              YearlyRevenueBudget = budget.YearlyRevenueBudget
                          });
            //SalesInvoice Line
            var SalesInvoice = (from invoiceHeader in _context.SalesInvoiceHeader
                                join invoiceLine in _context.SalesInvoiceLine
                                on invoiceHeader.SalesInvoiceHeaderId equals invoiceLine.SalesInvoiceHeaderId
                                join item in _context.Items
                                on invoiceLine.RecordNo equals item.ItemNo
                                where invoiceHeader.ShipmentDate.Value.Year == dateTime.Value.Year && invoiceHeader.DocumentType < 6
                                group new { invoiceLine, invoiceHeader, item } by new { category = invoiceLine.ItemCategoryCode} into result  
                                select new
                                { 
                                    category = result.Key.category,
                                    TodayRevenue = result.Sum(a=>a.invoiceHeader.ShipmentDate.Value.Date == dateTime.Value.Date ? a.invoiceLine.AmountIncludingVat : 0),
                                    TodayLiters = result.Sum(a=>a.invoiceHeader.ShipmentDate.Value.Date == dateTime.Value.Date ? a.invoiceLine.Quantity * a.item.LiterQty : 0),
                                    MonthlyRevenue = result.Sum(a=>a.invoiceHeader.ShipmentDate.Value.Month == dateTime.Value.Month && a.invoiceHeader.ShipmentDate.Value.Date <= dateTime.Value.Date ? a.invoiceLine.AmountIncludingVat : 0),
                                    MonthlyLiters = result.Sum(a=> a.invoiceHeader.ShipmentDate.Value.Month == dateTime.Value.Month && a.invoiceHeader.ShipmentDate.Value.Date <= dateTime.Value.Date ? a.invoiceLine.Quantity * a.item.LiterQty : 0),
                                    YearlyRevenue = result.Sum(a=>a.invoiceHeader.ShipmentDate.Value.Date <= dateTime.Value.Date ? a.invoiceLine.AmountIncludingVat : 0),
                                    YearlyLiters = result.Sum(a=> a.invoiceHeader.ShipmentDate.Value.Date <= dateTime.Value.Date ? a.invoiceLine.Quantity * a.item.LiterQty : 0)
                                });

            //Sales Cr Memo( untuk mengurangi yg diatas )
            var SalesCrMemo = (from crMemoHeader in _context.SalesCrMemoHeader
                               join crMemoLine in _context.SalesCrMemoLine
                               on crMemoHeader.SalesCrMemoHeaderId equals crMemoLine.SalesCrMemoHeaderId
                               join item in _context.Items
                               on crMemoLine.No equals item.ItemNo
                               where crMemoHeader.ShipmentDate.Value.Year == dateTime.Value.Year
                               group new { crMemoHeader, crMemoLine, item } by new { category = crMemoLine.ItemCategoryCode } into result
                               select new
                               {
                                   category = result.Key.category,
                                   TodayRevenue = result.Sum(a=>a.crMemoHeader.ShipmentDate.Value.Date == dateTime.Value.Date ? -1 * a.crMemoLine.AmountIncludingVat : 0),
                                   TodayLiters = result.Sum(a=>a.crMemoHeader.ShipmentDate.Value.Date == dateTime.Value.Date ? -1*(a.crMemoLine.Quantity*a.item.LiterQty) : 0),
                                   MonthlyRevenue = result.Sum(a=>a.crMemoHeader.ShipmentDate.Value.Month == dateTime.Value.Month && a.crMemoHeader.ShipmentDate.Value.Date <= dateTime.Value.Date ? -1 * a.crMemoLine.AmountIncludingVat : 0),
                                   MonthlyLiters = result.Sum(a=>a.crMemoHeader.ShipmentDate.Value.Month == dateTime.Value.Month && a.crMemoHeader.ShipmentDate.Value.Date <= dateTime.Value.Date ? -1 *(a.crMemoLine.Quantity*a.item.LiterQty) : 0),
                                   YearlyRevenue = result.Sum(a=>a.crMemoHeader.ShipmentDate.Value.Date <= dateTime.Value.Date ? a.crMemoLine.AmountIncludingVat*(-1) : 0),
                                   YearlyLiters = result.Sum(a=>a.crMemoHeader.ShipmentDate.Value.Date <= dateTime.Value.Date  ? (a.crMemoLine.Quantity*a.item.LiterQty)*(-1) : 0),
                               });
            //Dapatkan Sales Actual dari penjumlahan Invoice dan CrMemo
            var RevenueResult = from si in SalesInvoice
                                join cr in SalesCrMemo
                                on si.category equals cr.category into hasil
                                from cr in hasil.DefaultIfEmpty()
                                select new
                                {
                                category = si.category,
                                TodayRevenue = si.TodayRevenue + cr.TodayRevenue,
                                TodayLiters= si.TodayLiters + cr.TodayLiters,
                                MonthlyRevenue = si.MonthlyRevenue + cr.MonthlyRevenue,
                                MonthlyLiters = si.MonthlyLiters + cr.MonthlyLiters,
                                YearlyRevenue = si.YearlyRevenue + cr.YearlyRevenue,
                                YearlyLiters = si.YearlyLiters + cr.YearlyLiters
                                };

            var salesActual = (from budget in Budget
                               join revenue in RevenueResult
                               on budget.catrgory equals revenue.category into hasil
                               from revenue in hasil.DefaultIfEmpty()
                               select new {
                                   category = budget.catrgory,
                                   TodayRevenue = revenue.TodayRevenue ?? 0,
                                   TodayLiters = revenue.TodayLiters ?? 0,
                                   MonthlyRevenue = revenue.MonthlyRevenue ?? 0,
                                   MonthlyRevenueBudget = budget.MonthlyRevenueBudget ?? 0,
                                   MonthlyLiters = revenue.MonthlyLiters ?? 0,
                                   MonthlyLitersBudget = budget.MonthlyLitersBudget ?? 0,
                                   YearlyRevenue = revenue.YearlyRevenue ?? 0,
                                   YearlyRevenueBudget = budget.YearlyRevenueBudget ?? 0,
                                   YearlyLiters = revenue.YearlyLiters ?? 0,
                                   YearlyLitersBudget = budget.YearlyLitersBudget ?? 0
                               });
                                
            return Ok(salesActual);
        }
        
        public int getTotalDaysInMonthBMI(DateTime? dateTime)
        {
            if(dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            List<DateTime> Holidays = new List<DateTime>();
            Holidays = _context.ShopCalendarHoliday.Where(calendar => calendar.Date.Month == dateTime.Value.Month && calendar.Date.Year == dateTime.Value.Year).Select(calendar => calendar.Date).ToList();
            //int daysInMonth = DateTime.DaysInMonth(dateTime.Value.Year, dateTime.Value.Month);
            DateTime firstDate = new DateTime(dateTime.Value.Year, dateTime.Value.Month, 1);
            DateTime lastDate = firstDate.AddMonths(1).AddDays(-1);
            int totalDays = 0;
            for (DateTime dates = firstDate; dates <= lastDate; dates.AddDays(1))
            {
                if(dates.DayOfWeek != DayOfWeek.Sunday && !Holidays.Contains(dates))
                {
                    totalDays++;
                }
                dates = dates.AddDays(1);
            }           
            return totalDays;
        }
       
        public int getNoDaysInMonthBMI(DateTime? dateTime)
        {
            if(dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            List<DateTime> Holidays = new List<DateTime>();
            Holidays = _context.ShopCalendarHoliday.Where(calendar => calendar.Date.Month == dateTime.Value.Month && calendar.Date.Year == dateTime.Value.Year).Select(calendar => calendar.Date).ToList();
            DateTime firstDate = new DateTime(dateTime.Value.Year, dateTime.Value.Month, 1);
            DateTime lastDate = Convert.ToDateTime(dateTime);
            int totalDays = 0;
            for (DateTime dates = firstDate; dates <= lastDate; dates.AddDays(1))
            {
                if (dates.DayOfWeek != DayOfWeek.Sunday && !Holidays.Contains(dates))
                {
                    totalDays++;
                }
                dates = dates.AddDays(1);
            }
            return totalDays;

        }
    }
}
