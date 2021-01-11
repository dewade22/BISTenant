using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Manufacturing.Data.Entities
{
    public partial class Templates
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public int elementCount { get; set; }
    }
}
