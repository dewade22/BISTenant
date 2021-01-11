using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Manufacturing.Data.Entities
{
    public partial class Element
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
    }
}
