using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Manufacturing.Data.Entities
{
    public partial class DashboardLinkedElements
    {
        [Key]
        public int id { get; set; }
        public int DashboardId { get; set; }
        public int ElementId { get; set; }
        public string Placement { get; set; }
    }
}
