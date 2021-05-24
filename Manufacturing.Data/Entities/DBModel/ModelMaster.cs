using System;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Data.Entities
{ 
    public partial class ModelMaster
    {
        [Key]
        public int Id { get; set; }
        public string ModelId { get; set; }
        public string ModelName { get; set; }
        public string Description { get; set; }
        public string ProductID_SKUID { get; set; }
        public string VersionNo { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
