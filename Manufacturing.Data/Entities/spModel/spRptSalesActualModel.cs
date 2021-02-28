using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Manufacturing.Data.Entities
{
    public partial class spRptSalesActualModel
    {
        [Key]
        public string ItemCategory { get; set; }
        public int DaysNo { get; set; }
        public int DaysThisMonth { get; set; }
        public int DaysMonth { get; set; }
        public int DaysYear { get; set; }
        public Decimal LitersDay { get; set; }
        public Decimal LitersMonth { get; set; }
        public Decimal LitersYear { get; set; }
        public Decimal LitersBudget { get; set; }
        public Decimal LitersyearlyBudget { get; set; }
        public Decimal RevenueDay { get; set; }
        public Decimal RevenueMonth { get; set; }
        public Decimal RevenueYear { get; set; }
        public Decimal RevenueBudget { get; set; }
        public Decimal RevenueYearlyBudget { get; set; }
        public string BudgetMonth { get; set; }
    }
}
