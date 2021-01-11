using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manufacturing.Models.Items
{
    public class itemTableVM
    {
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public string ItemCategoryCode { get; set; }
        public string ProductGroupCode { get; set; }
        public string BaseUnitofMeasure { get; set; }
    }
}
