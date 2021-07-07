

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Data.Entities
{
    public partial class ModelWIPOutput
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        [Key]
        [Required]
        [StringLength(maximumLength:20)]
        public string ItemNo { get; set; }
        [StringLength(maximumLength:75)]
        public string Description { get; set; }
        [StringLength(maximumLength:10)]
        public string BaseUnitOfMeasure { get; set; }
        [StringLength(maximumLength:20)]
        public string InventoryPostingGroup { get; set; }
        public decimal? ItemCost { get; set; }
        public decimal? LastItemCost { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
