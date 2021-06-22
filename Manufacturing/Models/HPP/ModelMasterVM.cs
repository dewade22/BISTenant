using System.Collections.Generic;
using Manufacturing.Data.Entities;
using Manufacturing.Models.Hpp;

namespace Manufacturing.Models.Hpp
{
    public class ModelMasterVM
    {
        public ModelRateMaster detailMaster { get; set; }
        public IEnumerable<ModelMasterViewModel>ListMaster { get; set; }
    }
}
