using System;
using System.Collections.Generic;
using Manufacturing.Data.Entities;


namespace Manufacturing.Models.Hpp
{
    public class ModelRateViewModel
    {
        public ModelRateMaster rateMaster { get; set; }
        public IEnumerable<ModelRateMaster> lisRateMaster { get; set; }
    }
}
