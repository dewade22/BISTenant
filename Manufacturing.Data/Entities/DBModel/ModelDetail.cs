using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Data.Entities
{
    public partial class ModelDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string ModelDetailNo { get; set; }
        [Required]
        [StringLength(50)]
        public string ModelId { get; set; }
        [Required]
        [StringLength(150)]
        public string ProcessName { get; set; } = "";
        public string MachineId { get; set; } = "";
        public int? MachineQty { get; set; } = 0;
        public string TankType { get; set; } = "";
        public int? TankQty { get; set; } = 0;
        public string LaborType { get; set; } = "";
        public int? LaborQty { get; set; } = 0;
        public int? BatchSize { get; set; } = 0;
        public Decimal? LabourCapacity { get; set; } = 0;
        public Decimal? MachineCapacity { get; set; } = 0;
        public Decimal? MachineHour { get; set; } = 0;
        public string HourBase { get; set; } = "";
        public int? Operator { get; set; } = 0;
        public Decimal? ManHour { get; set; } = 0;
        public Decimal? FOHElectric { get; set; } = 0;
        public Decimal? ElectricUseDuration { get; set; } = 0;
        public Decimal? ElectricUseSpeed { get; set; } = 0;
        public Decimal? FOHGFW { get; set; } = 0;
        public string GFW { get; set; } = "";
        public string Description { get; set; } = "";
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; } = "";
    }
}
