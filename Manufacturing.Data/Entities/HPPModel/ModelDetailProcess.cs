using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Data.Entities
{
    public partial class ModelDetailProcess
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ModelId { get; set; }
        [Required]
        public string SubProcessId { get; set; }
        public int? ProcessHeaderNo { get; set; }
        public string Type { get; set; }
        public string ItemNo { get; set; }
        public string ItemDescription { get; set; }
        public string Description { get; set; }
        public decimal? ItemQty { get; set; }
        public decimal? ItemCost { get; set; }
        public decimal? ProcessHour { get; set; } = 0;
        public Boolean? Active { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
