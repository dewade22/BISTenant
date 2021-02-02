using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Data.Entities
{
    public partial class CountryRegion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryRegionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string EucountryRegionCode { get; set; }
        public string IntrastatCode { get; set; }
        public int? AddressFormat { get; set; }
        public int? ContactAddressFormat { get; set; }
        public short RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
    }
}
