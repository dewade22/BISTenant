using System;
using System.Collections.Generic;
using System.Text;

namespace Manufacturing.Data.Entities
{
    public partial class spRptSalesActual_LandedCostModel
    {
        public string TrxType { get; set; }
        public Decimal? LandedCost { get; set; }
        public Decimal? LandedCostMonth { get; set; }
        public Decimal? LandedCostYear { get; set; }
    }
}
