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

            var Monthly = TodayTransactionMonth(dateTime);
            var tes = TestingTransaksi(dateTime);

            //combine

            return new JsonResult( new{ MonthlyRev = Monthly, Category = tes});
        }

        

        public IActionResult TodayTransactionMonth(DateTime? dateTime)
        {
            DateTime firstDate = new DateTime(dateTime.Value.Year, dateTime.Value.Month, 1);
            var query = (from budget in _context.ItemBudgetEntry
                         where budget.Date.Value.Year == dateTime.Value.Year
                         group budget by new { category = budget.BudgetDimension2Code } into budgets
                         select new
                         {
                             ProductGroup = budgets.Key.category,
                             BudgetAmount = budgets.Sum(a => a.Date.Value.Month == dateTime.Value.Month ? a.SalesAmount : 0)
                         });
            var query2 = (from SI_Line in _context.SalesInvoiceLine
                         group SI_Line by new { category = SI_Line.ItemCategoryCode } into ress
                         select new
                         {
                             ProductGroup = ress.Key.category,
                             TodayRev = ress.Sum(a => a.ShipmentDate.Value.Date == dateTime.Value.Date ? a.AmountIncludingVat : 0),
                             UntilTodayRev = ress.Sum(a => a.ShipmentDate.Value.Date <= dateTime.Value.Date
                             && a.ShipmentDate.Value.Date >= firstDate.Date ? a.AmountIncludingVat : 0)
                         });
            var ressult = from q1 in query
                          join q2 in query2 on q1.ProductGroup equals q2.ProductGroup into hasil
                          from q2 in hasil.DefaultIfEmpty()
                          select new
                          {
                              ProductGroup = q1.ProductGroup,
                              TodayRev = q2.TodayRev ?? 0,
                              UntilTodayRev = q2.UntilTodayRev ?? 0,
                              BudgetAmount = q1.BudgetAmount ?? 0
                          };

            return Ok(ressult);
        }

        public IActionResult TestingTransaksi(DateTime? dateTime)
        {
            //Kategori
            var kategori = _context.DimensionValue.Where(a => a.DimensionCode.ToLower().Equals("category") && a.RowStatus == 0).Select(a=>a.DimensionValueName);

            //Item Budget Entry
            var budgetEntry = (from budget in _context.ItemBudgetEntry
                               where budget.Date.Value.Year == dateTime.Value.Year
                               group budget by new {category = budget.BudgetDimension2Code} into result
                               select new
                               {
                                   catrgory = result.Key.category,
                                   MonthlyLitersBudget = result.Sum(a=> a.Date.Value.Month == dateTime.Value.Month ? a.Liters : 0),
                                   MonthlyRevenueBudget = result.Sum(a=>a.Date.Value.Month == dateTime.Value.Month ? a.SalesAmount : 0),
                                   YearlyLitersBudget = result.Sum(a=>a.Liters ?? 0),
                                   YearlyRevenueBudget = result.Sum(a=>a.SalesAmount ?? 0)
                               });

            return Ok(budgetEntry);
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
