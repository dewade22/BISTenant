using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Data.Entities
{
    public partial class ModelRateMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string RateNo { get; set; }
        [Required]
        public string RateType { get; set; }
        [Required]
        public string RateName { get; set; }
        public Decimal? RegularRate { get; set; }
        public Decimal? Price { get; set; }
        public Decimal? LemburRate { get; set; }
        public Decimal? WeekendRate { get; set; }
        public string Unit { get; set; }
        public Decimal? SetupPrice { get; set; }
        public Decimal? PeakHourRate { get; set; }
        public Decimal? MaintenancePrice { get; set; }
        public int? Capacity { get; set; }
        public int? AgeUsedMonth { get; set; }
        public Decimal? SalvageValue { get; set; }
        [Required]
        public Boolean Active { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
        public Decimal? MOQ { get; set; }
    }
}
