using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Data.Entities
{
    public partial class ModelWIPProcessLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ModelWIPHeaderId { get; set; }
        [Key]
        public string ModelWIPLineId { get; set; }
        public string ItemType { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public Decimal? ItemQty { get; set; }
        public string ItemUnit { get; set; }
        public Decimal? ItemPrice { get; set; }
        public Decimal? ProcessHour { get; set; }
        public Boolean Active { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string lastModifiedBy { get; set; }

    }
}
