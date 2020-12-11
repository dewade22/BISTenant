using Manufacturing.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Data.Entities
{
    public class Item : BaseEntity
    {
        public int ItemID { get; set; } // int, not null
        [Column(TypeName = "varchar(20)")]
        public string ItemNo { get; set; } // varchar(20), not null
        //[Column(TypeName = "varchar(20)")]
        //public string ItemNo2 { get; set; } // varchar(20), null
        [Column(TypeName = "varchar(75)")]
        public string Description { get; set; } // varchar(75), null
        //[Column(TypeName = "varchar(75)")]
        //public string SearchDescription { get; set; } // varchar(75), null
        //[Column(TypeName = "varchar(75)")]
        //public string Description2 { get; set; } // varchar(75), null
        [Column(TypeName = "varchar(10)")]
        public string BaseUnitofMeasure { get; set; } // varchar(10), null
        //public int PriceUnitConversion { get; set; } // int, null
        //[Column(TypeName = "varchar(20)")]
        //public string InventoryPostingGroup { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(10)")]
        //public string ShelfNo { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string ItemDiscGroup { get; set; } // varchar(10), null
        //public byte AllowInvoiceDisc { get; set; } // tinyint, null
        //public int StatisticsGroup { get; set; } // int, null
        //public int CommissionGroup { get; set; } // int, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal UnitPrice { get; set; } // decimal(38,20), null
        //public int PriceProfitCalculation { get; set; } // int, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal ProfitPercent { get; set; } // decimal(38,20), null
        //public int CostingMethod { get; set; } // int, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal UnitCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal StandardCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal LastDirectCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal IndirectCostPercent { get; set; } // decimal(38,20), null
        //public byte CostisAdjusted { get; set; } // tinyint, null
        //public byte AllowOnlineAdjustment { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(20)")]
        //public string VendorNo { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string VendorItemNo { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string LeadTimeCalculation { get; set; } // varchar(32), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal ReorderPoint { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal MaximumInventory { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal ReorderQuantity { get; set; } // decimal(38,20), null
        //[Column(TypeName = "varchar(20)")]
        //public string AlternativeItemNo { get; set; } // varchar(20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal UnitListPrice { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal DutyDuePercent { get; set; } // decimal(38,20), null
        //[Column(TypeName = "varchar(10)")]
        //public string DutyCode { get; set; } // varchar(10), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal GrossWeight { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal NetWeight { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal UnitsperParcel { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal UnitVolume { get; set; } // decimal(38,20), null
        //[Column(TypeName = "varchar(10)")]
        //public string Durability { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string FreightType { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string TariffNo { get; set; } // varchar(10), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal DutyUnitConversion { get; set; } // decimal(38,20), null
        //[Column(TypeName = "varchar(10)")]
        //public string CountryPurchasedCode { get; set; } // varchar(10), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal BudgetQuantity { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal BudgetedAmount { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal BudgetProfit { get; set; } // decimal(38,20), null
        //public byte Blocked { get; set; } // tinyint, null
        //public byte PriceIncludesVAT { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(10)")]
        //public string VATBusPostingGrPrice { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string GenProdPostingGroup { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(100)")]
        //public string PictureFileName { get; set; } // varchar(100), null
        //[Column(TypeName = "varchar(10)")]
        //public string CountryofOriginCode { get; set; } // varchar(10), null
        //public byte AutomaticExtTexts { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(10)")]
        //public string NoSeries { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string TaxGroupCode { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string VATProdPostingGroup { get; set; } // varchar(10), null
        //public int Reserve { get; set; } // int, null
        //[Column(TypeName = "varchar(20)")]
        //public string GlobalDimension1Code { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string GlobalDimension2Code { get; set; } // varchar(20), null
        //public int LowLevelCode { get; set; } // int, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal LotSize { get; set; } // decimal(38,20), null
        //[Column(TypeName = "varchar(10)")]
        //public string SerialNos { get; set; } // varchar(10), null
        //public DateTime? LastUnitCostCalcDate { get; set; } // datetime, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal RolledupMaterialCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal RolledupCapacityCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal ScrapPercent { get; set; } // decimal(38,20), null
        //public byte InventoryValueZero { get; set; } // tinyint, null
        //public int DiscreteOrderQuantity { get; set; } // int, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal MinimumOrderQuantity { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal MaximumOrderQuantity { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal SafetyStockQuantity { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal OrderMultiple { get; set; } // decimal(38,20), null
        //[Column(TypeName = "varchar(20)")]
        //public string SafetyLeadTime { get; set; } // varchar(32), null
        //public int FlushingMethod { get; set; } // int, null
        //public int ReplenishmentSystem { get; set; } // int, null
        //public int RoundingPrecision { get; set; } // decimal(38,20), null
        //[Column(TypeName = "varchar(10)")]
        //public string SalesUnitofMeasure { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string PurchUnitofMeasure { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(20)")]
        //public string ReorderCycle { get; set; } // varchar(32), null
        //public int ReorderingPolicy { get; set; } // int, null
        //public byte IncludeInventory { get; set; } // tinyint, null
        //public int ManufacturingPolicy { get; set; } // int, null
        //[Column(TypeName = "varchar(10)")]
        //public string ManufacturerCode { get; set; } // varchar(10), null
        [Column(TypeName = "varchar(20)")]
        public string ItemCategoryCode { get; set; } // varchar(20), null
        //public byte CreatedFromNonstockItem { get; set; } // tinyint, null
        [Column(TypeName = "varchar(20)")]
        public string ProductGroupCode { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(10)")]
        //public string ServiceItemGroup { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string ItemTrackingCode { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string LotNos { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(20)")]
        //public string ExpirationCalculation { get; set; } // varchar(32), null
        //[Column(TypeName = "varchar(10)")]
        //public string SpecialEquipmentCode { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string PutawayTemplateCode { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string PutawayUnitofMeasureCode { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string PhysInvtCountingPeriodCode { get; set; } // varchar(10), null
        //public DateTime? LastCountingPeriodUpdate { get; set; } // datetime, null
        //[Column(TypeName = "varchar(20)")]
        //public string NextCountingPeriod { get; set; } // varchar(250), null
        //public byte UseCrossDocking { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(10)")]
        //public string DivisionCode { get; set; } // varchar(10), null
        //public DateTime? ModifyingDateofStatus { get; set; } // datetime, null
        //[Column(TypeName = "varchar(10)")]
        //public string ItemErrorCheckCode { get; set; } // varchar(10), null
        //public int ItemErrorCheckStatus { get; set; } // int, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal SuggestedQtyonPOS { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal ItemCapacityValue { get; set; } // decimal(38,20), null
        //public byte QtynotinDecimal { get; set; } // tinyint, null
        //public byte WeightItem { get; set; } // tinyint, null
        //public byte WarrantyCard { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(20)")]
        //public string DescriptiononPOS { get; set; } // varchar(20), null
        //public int DefOrderedby { get; set; } // int, null
        //public int DefOrderingMethod { get; set; } // int, null
        //[Column(TypeName = "varchar(20)")]
        //public string OriginalVendorNo { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string OriginalVendorItemNo { get; set; } // varchar(20), null
        //public byte NoStaffDiscountAllowed { get; set; } // tinyint, null
        //public byte NoCustomerDiscountAllowed { get; set; } // tinyint, null
        //public byte NoItemReturnAllowed { get; set; } // tinyint, null
        //public int BOMMethod { get; set; } // int, null
        //public int BOMReceiptPrint { get; set; } // int, null
        //[Column(TypeName = "varchar(20)")]
        //public string RecipeVersionCode { get; set; } // varchar(20), null
        //public int RecipeItemType { get; set; } // int, null
        //public int BOMCostPriceDistribution { get; set; } // int, null
        //public int BOMType { get; set; } // int, null
        //public int BOMReceivingExplode { get; set; } // int, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal Depth { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal Width { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal Height { get; set; } // decimal(38,20), null
        //[Column(TypeName = "varchar(20)")]
        //public string VariantFrameworkCode { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(10)")]
        //public string SeasonCode { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(20)")]
        //public string LifecycleLength { get; set; } // varchar(32), null
        //public DateTime? LifecycleStartingDate { get; set; } // datetime, null
        //public DateTime? LifecycleEndingDate { get; set; } // datetime, null
        //public byte ErrorCheckInternalUsage { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(30)")]
        //public string Attrib1Code { get; set; } // varchar(30), null
        //[Column(TypeName = "varchar(30)")]
        //public string Attrib2Code { get; set; } // varchar(30), null
        //[Column(TypeName = "varchar(30)")]
        //public string Attrib3Code { get; set; } // varchar(30), null
        //[Column(TypeName = "varchar(30)")]
        //public string Attrib4Code { get; set; } // varchar(30), null
        //[Column(TypeName = "varchar(30)")]
        //public string Attrib5Code { get; set; } // varchar(30), null
        //public int ABCSales { get; set; } // int, null
        //public int ABCProfit { get; set; } // int, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal WastagePercent { get; set; } // decimal(38,20), null
        //public byte ExcludedfromPortionWeight { get; set; } // tinyint, null
        //public byte UnaffbyMultiplFactor { get; set; } // tinyint, null
        //public byte ExcludefromMenuRequisition { get; set; } // tinyint, null
        //public int RecipeNoofPortions { get; set; } // int, null
        //public int MaxModifiersNoPrice { get; set; } // int, null
        //public int MaxIngrRemovedNoPrice { get; set; } // int, null
        //public int MaxIngrModifiers { get; set; } // int, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal ProductionTimeMin { get; set; } // decimal(38,20), null
        //public byte DisplayIngredientsinMonitor { get; set; } // tinyint, null
        //public byte DisplayInstructinMonitor { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(10)")]
        //public string RecipeMainIngredient { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string RecipeStyle { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string RecipeCategory { get; set; } // varchar(10), null
        //public byte AvailableasDish { get; set; } // tinyint, null
        //public byte UOMPopuponPOS { get; set; } // tinyint, null
        //public int ReplenishmentCalculationType { get; set; } // int, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal ManualEstimatedDailySale { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal StoreStockCoverReqdDays { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal WarehStockCoverReqdDays { get; set; } // decimal(38,20), null
        //[Column(TypeName = "varchar(10)")]
        //public string ReplenishmentSalesProfile { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string ReplenishmentGradeCode { get; set; } // varchar(10), null
        //public byte NotActiveforReplenishment { get; set; } // tinyint, null
        //public byte ExcludefromAutomReplenishm { get; set; } // tinyint, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal TransferMultiple { get; set; } // decimal(38,20), null
        //public byte RangeinLocation { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(10)")]
        //public string StoreForwardSalesProfile { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string WarehForwardSalesProfile { get; set; } // varchar(10), null
        //public int PurchOrderDelivery { get; set; } // int, null
        //[Column(TypeName = "varchar(20)")]
        //public string ReplenishasItemNo { get; set; } // varchar(20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal ProfitGoalPercent { get; set; } // decimal(38,20), null
        //[Column(TypeName = "varchar(10)")]
        //public string ReplenDataProfile { get; set; } // varchar(10), null
        //public int LikeforLikeReplenMethod { get; set; } // int, null
        //public int LikeforLikeProcessMethod { get; set; } // int, null
        //public int ReplenishasItemNoMethod { get; set; } // int, null
        //[Column(TypeName = "varchar(10)")]
        //public string ReplenDistributionRuleCode { get; set; } // varchar(10), null
        //public byte SelectLowestPriceVendor { get; set; } // tinyint, null
        //public int EffectiveInvSalesOrder { get; set; } // int, null
        //public int EffectiveInvPurchaseOrd { get; set; } // int, null
        //public int EffectiveInvTransferInb { get; set; } // int, null
        //public int EffectiveInvTransferOutb { get; set; } // int, null
        //public byte FuelItem { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(20)")]
        //public string RoutingNo { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string ProductionBOMNo { get; set; } // varchar(20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal SingleLevelMaterialCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal SingleLevelCapacityCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal SingleLevelSubcontrdCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal SingleLevelCapOvhdCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal SingleLevelMfgOvhdCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal OverheadRate { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal RolledupSubcontractedCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal RolledupMfgOvhdCost { get; set; } // decimal(38,20), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal RolledupCapOverheadCost { get; set; } // decimal(38,20), null
        //public int OrderTrackingPolicy { get; set; } // int, null
        //public byte Critical { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(10)")]
        //public string ItemFamilyCode { get; set; } // varchar(10), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal UnitPriceIncludingVAT { get; set; } // decimal(38,20), null
        //public int POSCostCalculation { get; set; } // int, null
        //public byte NoStockPosting { get; set; } // tinyint, null
        //public byte ZeroPriceValid { get; set; } // tinyint, null
        //public byte QtyBecomesNegative { get; set; } // tinyint, null
        //public byte NoDiscountAllowed { get; set; } // tinyint, null
        //public int KeyinginPrice { get; set; } // int, null
        //public byte ScaleItem { get; set; } // tinyint, null
        //public int KeyinginQuantity { get; set; } // int, null
        //public byte SkipCompressionWhenScanned { get; set; } // tinyint, null
        //public byte SkipCompressionWhenPrinted { get; set; } // tinyint, null
        //public string BarcodeMask { get; set; } // varchar(22), null
        //public byte UseEANStandardBarc { get; set; } // tinyint, null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal QtyperBaseCompUnit { get; set; } // decimal(38,20), null
        //[Column(TypeName = "varchar(10)")]
        //public string BaseCompUnitCode { get; set; } // varchar(10), null
        //[Column(TypeName = "varchar(10)")]
        //public string ComparisonUnitCode { get; set; } // varchar(10), null
        //[Required]
        //[Column(TypeName = "decimal(20, 5)")]
        //public decimal CompPriceInclVAT { get; set; } // decimal(38,20), null
        //public byte ExplodeBOMinStatemPosting { get; set; } // tinyint, null
        //public byte DisableDispensePrinting { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(10)")]
        //public string DispensePrinterGroup { get; set; } // varchar(10), null
        //public byte PrintVariantsShelfLabels { get; set; } // tinyint, null
        //[Column(TypeName = "varchar(20)")]
        //public string CommonItemNo { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string WarrantyPeriod { get; set; } // varchar(32), null
        //[Column(TypeName = "varchar(20)")]
        //public string DimensionValue01Code { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string DimensionValue02Code { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string DimensionValue03Code { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string DimensionValue04Code { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string DimensionValue05Code { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string DimensionValue06Code { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string DimensionValue07Code { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string DimensionValue08Code { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string DimensionValue09Code { get; set; } // varchar(20), null
        //[Column(TypeName = "varchar(20)")]
        //public string DimensionValue10Code { get; set; } // varchar(20), null
        //public short ForSale { get; set; } // smallint, null
        //public short ForPurchase { get; set; } // smallint, null
        //public short ForInventory { get; set; } // smallint, null

        public Item()
        {
            this.ItemNo = "";
            //this.ItemNo2 = "";
            this.Description = "";
            //this.SearchDescription = "";
            //this.Description2 = "";
            this.BaseUnitofMeasure = "";
            //this.PriceUnitConversion = 0;
            //this.InventoryPostingGroup = "";
            //this.ShelfNo = "";
            //this.ItemDiscGroup = "";
            //this.AllowInvoiceDisc = 0;
            //this.StatisticsGroup = 0;
            //this.CommissionGroup = 0;
            //this.UnitPrice = 0;
            //this.PriceProfitCalculation = 0;
            //this.ProfitPercent = 0;
            //this.CostingMethod = 0;
            //this.UnitCost = 0;
            //this.StandardCost = 0;
            //this.LastDirectCost = 0;
            //this.IndirectCostPercent = 0;
            //this.CostisAdjusted = 0;
            //this.AllowOnlineAdjustment = 0;
            //this.VendorNo = "";
            //this.VendorItemNo = "";
            //this.LeadTimeCalculation = "";
            //this.ReorderPoint = 0;
            //this.MaximumInventory = 0;
            //this.ReorderQuantity = 0;
            //this.AlternativeItemNo = "";
            //this.UnitListPrice = 0;
            //this.DutyDuePercent = 0;
            //this.DutyCode = "";
            //this.GrossWeight = 0;
            //this.NetWeight = 0;
            //this.UnitsperParcel = 0;
            //this.UnitVolume = 0;
            //this.Durability = "";
            //this.FreightType = "";
            //this.TariffNo = "";
            //this.DutyUnitConversion = 0;
            //this.CountryPurchasedCode = "";
            //this.BudgetQuantity = 0;
            //this.BudgetedAmount = 0;
            //this.BudgetProfit = 0;
            //this.Blocked = 1;
            //this.PriceIncludesVAT = 0;
            //this.VATBusPostingGrPrice = "";
            //this.GenProdPostingGroup = "";
            //this.PictureFileName = "";
            //this.CountryofOriginCode = "";
            //this.AutomaticExtTexts = 0;
            //this.NoSeries = "";
            //this.TaxGroupCode = "";
            //this.VATProdPostingGroup = "";
            //this.Reserve = 0;
            //this.GlobalDimension1Code = "";
            //this.GlobalDimension2Code = "";
            //this.LowLevelCode = 0;
            //this.LotSize = 1;
            //this.SerialNos = "";
            //this.LastUnitCostCalcDate = null;
            //this.RolledupMaterialCost = 0;
            //this.RolledupCapacityCost = 0;
            //this.ScrapPercent = 0;
            //this.InventoryValueZero = 0;
            //this.DiscreteOrderQuantity = 0;
            //this.MinimumOrderQuantity = 0;
            //this.MaximumOrderQuantity = 0;
            //this.SafetyStockQuantity = 0;
            //this.OrderMultiple = 0;
            //this.SafetyLeadTime = "";
            //this.FlushingMethod = 0;
            //this.ReplenishmentSystem = 0;
            //this.RoundingPrecision = 0;
            //this.SalesUnitofMeasure = "";
            //this.PurchUnitofMeasure = "";
            //this.ReorderCycle = "";
            //this.ReorderingPolicy = 0;
            //this.IncludeInventory = 0;
            //this.ManufacturingPolicy = 0;
            //this.ManufacturerCode = "";
            this.ItemCategoryCode = "";
            //this.CreatedFromNonstockItem = 0;
            this.ProductGroupCode = "";
            //this.ServiceItemGroup = "";
            //this.ItemTrackingCode = "";
            //this.LotNos = "";
            //this.ExpirationCalculation = "";
            //this.SpecialEquipmentCode = "";
            //this.PutawayTemplateCode = "";
            //this.PutawayUnitofMeasureCode = "";
            //this.PhysInvtCountingPeriodCode = "";
            //this.LastCountingPeriodUpdate = null;
            //this.NextCountingPeriod = "";
            //this.UseCrossDocking = 0;
            //this.DivisionCode = "";
            //this.ModifyingDateofStatus = null;
            //this.ItemErrorCheckCode = "";
            //this.ItemErrorCheckStatus = 0;
            //this.SuggestedQtyonPOS = 0;
            //this.ItemCapacityValue = 0;
            //this.QtynotinDecimal = 0;
            //this.WeightItem = 0;
            //this.WarrantyCard = 0;
            //this.DescriptiononPOS = "";
            //this.DefOrderedby = 0;
            //this.DefOrderingMethod = 0;
            //this.OriginalVendorNo = "";
            //this.OriginalVendorItemNo = "";
            //this.NoStaffDiscountAllowed = 0;
            //this.NoCustomerDiscountAllowed = 0;
            //this.NoItemReturnAllowed = 0;
            //this.BOMMethod = 0;
            //this.BOMReceiptPrint = 0;
            //this.RecipeVersionCode = "";
            //this.RecipeItemType = 0;
            //this.BOMCostPriceDistribution = 0;
            //this.BOMType = 0;
            //this.BOMReceivingExplode = 0;
            //this.Depth = 0;
            //this.Width = 0;
            //this.Height = 0;
            //this.VariantFrameworkCode = "";
            //this.SeasonCode = "";
            //this.LifecycleLength = "";
            //this.LifecycleStartingDate = null;
            //this.LifecycleEndingDate = null;
            //this.ErrorCheckInternalUsage = 0;
            //this.Attrib1Code = "";
            //this.Attrib2Code = "";
            //this.Attrib3Code = "";
            //this.Attrib4Code = "";
            //this.Attrib5Code = "";
            //this.ABCSales = 0;
            //this.ABCProfit = 0;
            //this.WastagePercent = 0;
            //this.ExcludedfromPortionWeight = 0;
            //this.UnaffbyMultiplFactor = 0;
            //this.ExcludefromMenuRequisition = 0;
            //this.RecipeNoofPortions = 0;
            //this.MaxModifiersNoPrice = 0;
            //this.MaxIngrRemovedNoPrice = 0;
            //this.MaxIngrModifiers = 0;
            //this.ProductionTimeMin = 0;
            //this.DisplayIngredientsinMonitor = 0;
            //this.DisplayInstructinMonitor = 0;
            //this.RecipeMainIngredient = "";
            //this.RecipeStyle = "";
            //this.RecipeCategory = "";
            //this.AvailableasDish = 0;
            //this.UOMPopuponPOS = 0;
            //this.ReplenishmentCalculationType = 0;
            //this.ManualEstimatedDailySale = 0;
            //this.StoreStockCoverReqdDays = 0;
            //this.WarehStockCoverReqdDays = 0;
            //this.ReplenishmentSalesProfile = "";
            //this.ReplenishmentGradeCode = "";
            //this.NotActiveforReplenishment = 0;
            //this.ExcludefromAutomReplenishm = 0;
            //this.TransferMultiple = 0;
            //this.RangeinLocation = 0;
            //this.StoreForwardSalesProfile = "";
            //this.WarehForwardSalesProfile = "";
            //this.PurchOrderDelivery = 0;
            //this.ReplenishasItemNo = "";
            //this.ProfitGoalPercent = 0;
            //this.ReplenDataProfile = "";
            //this.LikeforLikeReplenMethod = 0;
            //this.LikeforLikeProcessMethod = 0;
            //this.ReplenishasItemNoMethod = 0;
            //this.ReplenDistributionRuleCode = "";
            //this.SelectLowestPriceVendor = 0;
            //this.EffectiveInvSalesOrder = 0;
            //this.EffectiveInvPurchaseOrd = 0;
            //this.EffectiveInvTransferInb = 0;
            //this.EffectiveInvTransferOutb = 0;
            //this.FuelItem = 0;
            //this.RoutingNo = "";
            //this.ProductionBOMNo = "";
            //this.SingleLevelMaterialCost = 0;
            //this.SingleLevelCapacityCost = 0;
            //this.SingleLevelSubcontrdCost = 0;
            //this.SingleLevelCapOvhdCost = 0;
            //this.SingleLevelMfgOvhdCost = 0;
            //this.OverheadRate = 0;
            //this.RolledupSubcontractedCost = 0;
            //this.RolledupMfgOvhdCost = 0;
            //this.RolledupCapOverheadCost = 0;
            //this.OrderTrackingPolicy = 0;
            //this.Critical = 0;
            //this.ItemFamilyCode = "";
            //this.UnitPriceIncludingVAT = 0;
            //this.POSCostCalculation = 0;
            //this.NoStockPosting = 0;
            //this.ZeroPriceValid = 0;
            //this.QtyBecomesNegative = 0;
            //this.NoDiscountAllowed = 0;
            //this.KeyinginPrice = 0;
            //this.ScaleItem = 0;
            //this.KeyinginQuantity = 0;
            //this.SkipCompressionWhenScanned = 0;
            //this.SkipCompressionWhenPrinted = 0;
            //this.BarcodeMask = "";
            //this.UseEANStandardBarc = 0;
            //this.QtyperBaseCompUnit = 0;
            //this.BaseCompUnitCode = "";
            //this.ComparisonUnitCode = "";
            //this.CompPriceInclVAT = 0;
            //this.ExplodeBOMinStatemPosting = 0;
            //this.DisableDispensePrinting = 0;
            //this.DispensePrinterGroup = "";
            //this.PrintVariantsShelfLabels = 0;
            //this.CommonItemNo = "";
            //this.WarrantyPeriod = "";
            //this.DimensionValue01Code = "";
            //this.DimensionValue02Code = "";
            //this.DimensionValue03Code = "";
            //this.DimensionValue04Code = "";
            //this.DimensionValue05Code = "";
            //this.DimensionValue06Code = "";
            //this.DimensionValue07Code = "";
            //this.DimensionValue08Code = "";
            //this.DimensionValue09Code = "";
            //this.DimensionValue10Code = "";
            //this.ForSale = 0;
            //this.ForPurchase = 0;
            //this.ForInventory = 0;
        }
    }
}
