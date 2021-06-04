using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manufacturing.Models.Hpp
{
    public class MachineViewModel
    {
        [BindProperty]
        public Manufacturing.Data.Entities.ModelMachineType MachineType { get; set; }
        [BindProperty]
        public Manufacturing.Data.Entities.ModelMachineMaster MachineMaster { get; set; }
    }
}
