using Manufacturing.Data.Entities;
using System.Collections.Generic;

namespace Manufacturing.Models.Hpp
{
    public class ModelPraMixing
    {
        public ModelWIPProcessHeader header { get; set; }
        public ModelWIPProcessLine line { get; set; }
        public IEnumerable<ModelWIPProcessHeader> listHeader { get; set; }
        public IEnumerable<ModelWIPProcessLine> listLine { get; set; }
    }
}
