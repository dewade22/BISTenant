using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Manufacturing.Data.Entities
{
    public partial class Dashboards_Info
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order=0)]
        public int id { get; set; }
        public string Name { get; set; }
        public int TemplateId { get; set; }

    }
}
