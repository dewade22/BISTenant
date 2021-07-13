using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Data.Entities
{
    public partial class ModelDetailProcessHeader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ModelId { get; set; }
        public string Description { get; set; }
        public decimal ProcessSize { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
