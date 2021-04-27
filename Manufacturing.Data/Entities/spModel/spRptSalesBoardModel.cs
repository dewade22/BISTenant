using System;


namespace Manufacturing.Data.Entities
{
    public partial class spRptSalesBoardModel
    {
        public string ItemCategory { get; set; }
        public string SalesPerson { get; set; }
        public string BudgetMonth { get; set; }
        public int? DaysNo { get; set; }
        public int? DaysMonth { get; set; }
        public Decimal? LitersDay { get; set; }
        public Decimal? LitersMonth { get; set; }
        public Decimal? LitersBudget { get; set; }
        public Decimal? RevenueDay { get; set; }
        public Decimal? RevenueMonth { get; set; }
        public Decimal? RevenueBudget { get; set; }
        public Decimal? QtyDay { get; set; }
        public Decimal? QtyMonth { get; set; }
        public Decimal? QtyBudget { get; set; }
    }
}
