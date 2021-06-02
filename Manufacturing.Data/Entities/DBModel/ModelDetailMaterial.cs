using System;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Data.Entities
{
    public partial class ModelDetailMaterial
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ModelId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ModelDetailNo { get; set; }
        [Required]
        public string MatID { get; set; }
        [Required]
        public decimal QtyMatID { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
