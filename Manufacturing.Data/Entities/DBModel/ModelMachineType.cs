using System;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Data.Entities
{
    public partial class ModelMachineType
    {
        public int Id { get; set; }
        [Required]
        public string MachineTypeNo { get; set; }
        [Required]
        public string MachineTypeName { get; set; }
        public string MachineTypeDescription { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public Boolean Active { get; set; } = true;
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
