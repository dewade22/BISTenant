
namespace Manufacturing.Models.Hpp
{
    public class ModelPraMixing
    {
        public string Machine { get; set; } = "";
        public int? Qty { get; set; } = 0;
        public decimal? Capacity { get; set; } = 0;
        public int? Labour { get; set; } = 0;
        public int? Batch { get; set; } = 0;
        public int? LabourHour { get; set; } = 0;
        public string FOHType { get; set; } = "";
        public int? FOH { get; set; } = 0;
    }
}
