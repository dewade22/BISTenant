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
        public string RateName { get; set; } = "";
        public Decimal? RegularRate { get; set; } = 0;
        public Decimal? Price { get; set; } = 0;
        public Decimal? LemburRate { get; set; } = 0;
        public Decimal? WeekendRate { get; set; } = 0;
        public string Unit { get; set; } = "";
        public Decimal? SetupPrice { get; set; } = 0;
        public Decimal? PeakHourRate { get; set; } = 0;
        public Decimal? MaintenancePrice { get; set; } = 0;
        public int? Capacity { get; set; } = 0;
        public int? AgeUsedMonth { get; set; } = 0;
        public Decimal? SalvageValue { get; set; } = 0;
        [Required]
        public Boolean Active { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
        public Decimal? MOQ { get; set; } = 0;
    }
}
