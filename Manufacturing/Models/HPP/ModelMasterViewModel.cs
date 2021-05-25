using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manufacturing.Data.Entities;

namespace Manufacturing.Models.Hpp
{
    public class ModelMasterViewModel
    {
        public Manufacturing.Data.Entities.Items itemTable { get; set; }
        public Manufacturing.Data.Entities.ModelMaster masterModel { get; set; }
    }
}