using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Data.Entities
{
    public partial class ModelWIPProcessHeader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string ModelHeaderId { get; set; }
        [Required]
        public string ItemOutputId { get; set; }
        public string Description { get; set; } = "";
        public string ItemOutputName { get; set; }
        [Required]
        public Decimal QtyOutput { get; set; }
        public string QtyOutputUnit { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
