using System.Collections.Generic;
using Manufacturing.Data.Entities;

namespace Manufacturing.Models.Hpp
{
    public class ModelMasterVM
    {
        public ModelRateMaster detailMaster { get; set; }
        public IEnumerable<ModelMasterViewModel>ListMaster { get; set; }
        public ModelRateMaster rateMaster { get; set; }
        public ModelPraMixing praMixing { get; set; }
        public IEnumerable<ModelWIPProcessHeader> ListWipHeader { get; set; }
        public IEnumerable<Manufacturing.Data.Entities.Items> ListItems { get; set; }
    }
}
