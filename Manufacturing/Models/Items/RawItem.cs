using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manufacturing.Models.Items
{
    public class RawItem
    {
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public string BaseUnitofMeasure { get; set; }
        public decimal? Quantity { get; set; }
    }
}
