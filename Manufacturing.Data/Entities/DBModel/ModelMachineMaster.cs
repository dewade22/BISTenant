using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Data.Entities
{
    public partial class ModelMachineMaster
    {
        public int Id { get; set; }
        [Required]
        public string MachineNo { get; set; }
        [Required]
        [StringLength(250, MinimumLength =3)]
        public string MachineName { get; set; }
        [Required]
        public string MachineType { get; set; }
        [Required]
        public decimal MachinePrice { get; set; } = 0;
        [Required]
        public decimal MachineSpeed { get; set; }
        [Required]
        public decimal MachineSetupPrice { get; set; } = 0;
        [Required]
        public decimal MachineMaintenancePrice { get; set; } = 0;
        [Required]
        public decimal MaximumAgeUse { get; set; } = 0;
        [Required]
        public decimal SalvageValue { get; set; } = 0;
        [Required]
        public decimal PowerConsumption { get; set; } = 0;
        [Required]
        public Boolean Active { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }

    }
}
