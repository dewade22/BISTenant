using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manufacturing.Models
{
    public class ValuationFilter
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string invenRPT { get; set; }
        public string category { get; set; }
        public string prodGroups { get; set; }
        public string location { get; set; }
        public string Flavour { get; set; }
        public string Size { get; set; }
    }
}
