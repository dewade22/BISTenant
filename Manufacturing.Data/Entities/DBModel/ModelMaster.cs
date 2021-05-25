using System;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Data.Entities
{ 
    public partial class ModelMaster
    {
        [Key]
        public int Id { get; set; }
        public string ModelId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ModelName { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public string ProductID_SKUID { get; set; }
        [StringLength(50)]
        public string VersionNo { get; set; }
        public Boolean Active { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
