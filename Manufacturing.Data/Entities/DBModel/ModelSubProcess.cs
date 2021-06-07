using System;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Data.Entities
{
    public partial class ModelSubProcess
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SubProcessId { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string SubProcessName { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
        [Required]
        public Boolean Active { get; set; }
    }
}
