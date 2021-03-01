using System;
using System.Collections.Generic;
using System.Text;

namespace Manufacturing.Data.Entities
{
    public partial class spSalesInvoiceSummaryPivotModel
    {
        public string Trx { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string SalesPerson { get; set; }
        public string DocumentNo { get; set; }
        public string SONumber { get; set; }
        public string BilltoName { get; set; }
        public Decimal Qty { get; set; }
        public Decimal Liters { get; set; }
        public Decimal Cost { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Discount { get; set; }
        public Decimal Tax { get; set; }
        public Decimal AmountIncdTax { get; set; }
        public Decimal LandedCost { get; set; }
        public Decimal Revenue { get; set; }
        public string Category { get; set; }
        public string Packaging { get; set; }
        public string Flavours { get; set; }
        public Decimal Liters_Sub { get; set; }
        public Decimal AmountIncdTax_Sub { get; set; }
    }
}
