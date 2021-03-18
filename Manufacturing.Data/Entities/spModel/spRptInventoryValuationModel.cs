using System;
using System.Collections.Generic;
using System.Text;

namespace Manufacturing.Data.Entities
{
    public partial class spRptInventoryValuationModel
    {
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public string UOM { get; set; }
        public string InventoryPostingGroup { get; set; }
        public string ItemCategoryCode { get; set; }
        public string ProductGroupCode { get; set; }
        public string Sizes { get; set; }
        public string ItemFlavour { get; set; }
        public Decimal? QtyStarting { get; set; }
        public Decimal? LiterStarting { get; set; }
        public Decimal? ValueStarting { get; set; }
        public Decimal? QtyIn { get; set; }
        public Decimal? LiterIn { get; set; }
        public Decimal? ValueIn { get; set; }
        public Decimal? QtyOut { get; set; }
        public Decimal? LiterOut { get; set; }
        public Decimal? ValueOut { get; set; }
        public Decimal? QtyEnding { get; set; }
        public Decimal? LiterEnding { get; set; }
        public Decimal? ValueEnding { get; set; }

    }
}
