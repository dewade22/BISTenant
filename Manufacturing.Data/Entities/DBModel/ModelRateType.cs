using System;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Data.Entities
{
    public partial class ModelRateType
    {
        public int Id { get; set; }
        [Key]
        public string RateTypeCode { get; set; }
        public string RateTypeName { get; set; }
        [Required]
        public Boolean Active { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
