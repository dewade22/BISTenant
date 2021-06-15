using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Data.Entities
{
    public partial class UnitOfMeasures
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitOfMeasureID { get; set; }
        [Key]
        public string UOMCode { get; set; }
        public string UOMDescription { get; set; }
        public Boolean? DefaultUnitOfMeasure { get; set; }
        public Byte? WeightUnitOfMeasure { get; set; }
        public Decimal? POSMinDenominator { get; set; }
        public Int16? RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
    }
}
