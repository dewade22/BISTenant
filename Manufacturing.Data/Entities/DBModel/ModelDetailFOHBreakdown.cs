using System;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Data.Entities
{
    public partial class ModelDetailFOHBreakdown
    {
        public int Id { get; set; }
        [Required]
        public string ModelId { get; set; }
        [Required]
        public string ModelDetailFOHNo { get; set; }
        [Required]
        public string FOHType { get; set; }
        [Required]
        public string SPID { get; set; }
        [Required]
        public int SubProcessSize { get; set; }
        public string OperationName { get; set; }
        public string SPMachineID { get; set; }
        public Decimal? SPSpeed { get; set; }
        public Decimal? SPDuration { get; set; }
        public Decimal? SPQuantity { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public Boolean Active { get; set; } = true;
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
