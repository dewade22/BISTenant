

namespace Manufacturing.Models.Hpp
{
    public class TableFOHViewModel
    {
        public string ModelId { get; set; }
        public string FOHType { get; set; }
        public int SubProcessSize { get; set; }
        public string OperationName { get; set; }
        public string SPMachineID { get; set; }
        public decimal? SPSpeed { get; set; }
        public decimal? SPDuration { get; set; }
        public decimal? SPQuantity { get; set; }
        public string MachineName { get; set; }
        public decimal? PowerConsumption { get; set; }
        public decimal? FOHAmount { get; set; }
    }
}
