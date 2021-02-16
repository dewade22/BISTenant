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
            
           

            return new JsonResult( new{ MonthlyRev = SalesBudget});
        }

        

        public IActionResult TodayTransactionMonth(DateTime? dateTime)
        {
            DateTime firstDate = new DateTime(dateTime.Value.Year, dateTime.Value.Month, 1);
            //Kategori
            var kategori = _context.DimensionValue.Where(a => a.DimensionCode.ToLower().Equals("category") && a.RowStatus == 0).Select(a => a.DimensionValueName);

            //Item Budget Entry
            var budgetEntry = (from budget in _context.ItemBudgetEntry
                               where budget.Date.Value.Year == dateTime.Value.Year
                               group budget by new { category = budget.BudgetDimension2Code } into result
                               select new
                               {
                                   catrgory = result.Key.category,
                                   MonthlyLitersBudget = result.Sum(a => a.Date.Value.Month == dateTime.Value.Month ? a.Liters : 0),
                                   MonthlyRevenueBudget = result.Sum(a => a.Date.Value.Month == dateTime.Value.Month ? a.SalesAmount : 0),
                                   YearlyLitersBudget = result.Sum(a => a.Liters ?? 0),
                                   YearlyRevenueBudget = result.Sum(a => a.SalesAmount ?? 0)
                               });

            //SalesInvoice Line
            var SalesInvoice = (from invoiceHeader in _context.SalesInvoiceHeader
                                join invoiceLine in _context.SalesInvoiceLine
                                on invoiceHeader.SalesInvoiceHeaderId equals invoiceLine.SalesInvoiceHeaderId
                                join item in _context.Items
                                on invoiceLine.RecordNo equals item.ItemNo
                                where invoiceHeader.ShipmentDate.Value.Year == dateTime.Value.Year
                                group new { invoiceLine, invoiceHeader, item } by new { category = invoiceLine.ItemCategoryCode} into result  
                                select new
                                { 
                                    category = result.Key.category,
                                    TodayRevenue = result.Sum(a=>a.invoiceHeader.ShipmentDate.Value.Date == dateTime.Value.Date ? a.invoiceLine.AmountIncludingVat : 0),
                                    TodayLiters = result.Sum(a=>a.invoiceHeader.ShipmentDate.Value.Date == dateTime.Value.Date ? a.invoiceLine.Quantity * a.item.LiterQty : 0),
                                    MonthlyRevenue = result.Sum(a=>a.invoiceHeader.ShipmentDate.Value.Month == dateTime.Value.Month && a.invoiceHeader.ShipmentDate.Value.Day <= dateTime.Value.Day ? a.invoiceLine.AmountIncludingVat : 0),
                                    MonthlyLiters = result.Sum(a=>a.invoiceHeader.ShipmentDate.Value.Month == dateTime.Value.Month ? a.invoiceLine.Quantity * a.item.LiterQty : 0),
                                    YearlyRevenue = result.Sum(a=>a.invoiceLine.AmountIncludingVat ?? 0),
                                    YearlyLiters = result.Sum(a=>a.invoiceLine.Quantity * a.item.LiterQty ?? 0)
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
                                   MonthlyRevenue = result.Sum(a=>a.crMemoHeader.ShipmentDate.Value.Month == dateTime.Value.Month && a.crMemoHeader.ShipmentDate.Value.Day <= dateTime.Value.Day ? -1 * a.crMemoLine.AmountIncludingVat : 0),
                                   MonthlyLiters = result.Sum(a=>a.crMemoHeader.ShipmentDate.Value.Month == dateTime.Value.Month ? -1 *(a.crMemoLine.Quantity*a.item.LiterQty) : 0),
                                   YearlyRevenue = result.Sum(a=>a.crMemoLine.AmountIncludingVat*(-1) ?? 0),
                                   YearlyLiters = result.Sum(a=>(a.crMemoLine.Quantity*a.item.LiterQty)*(-1) ?? 0),
                               });

            var RevenueResult = SalesInvoice.Concat(SalesCrMemo);
                                
            return Ok(RevenueResult);
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
