using Manufacturing.Data.Entities;
using System.Collections.Generic;

namespace Manufacturing.Models.Hpp
{
    public class ModelMixing
    {
        public ModelMaster master { get; set; }
        public ModelWIPProcessHeader header { get; set; }
        public ModelWIPProcessLine line { get; set; }
        public ModelDetailProcess detailProcess { get; set; }
        public ModelDetailProcessHeader detailProcessHeader { get; set; }
        public ModelDetailFOHBreakdown fOHBreakdown { get; set; }
        public IEnumerable<ModelWIPProcessHeader> listHeader { get; set; }
        public IEnumerable<ModelWIPProcessLine> listLine { get; set; }
        public IEnumerable<TableFOHViewModel> listTableFOH { get; set; }
        public IEnumerable<ModelDetailProcess> listDetailProcess { get; set; }
    }
}
