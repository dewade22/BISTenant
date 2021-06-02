using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manufacturing.Models.Hpp
{
    public class ModelMasterDetailMaterialVM
    {
        public Manufacturing.Data.Entities.ModelMaster masterModel { get; set; }
        public Manufacturing.Data.Entities.ModelDetailMaterial detailMaterial { get; set; }
        public Manufacturing.Data.Entities.Items Items {get; set;}
    }
}
