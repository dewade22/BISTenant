using System;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Models
{
    public class ValuationFilter
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string invenRPT { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string category { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string prodGroups { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string location { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Flavour { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Size { get; set; } = "";
    }
}
