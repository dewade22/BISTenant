using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manufacturing.Models.Hpp
{
    public class MachineViewModel
    {
        
        public Manufacturing.Data.Entities.ModelMachineType MachineType { get; set; }
        
        public Manufacturing.Data.Entities.ModelMachineMaster MachineMaster { get; set; }
        
        public Manufacturing.Data.Entities.ModelSubProcess ModelSubProcess { get; set; }
        
        public Manufacturing.Data.Entities.ModelDetailFOHBreakdown ModelDetailFOHBreakdown { get; set; }
    }
}
