﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Data.Entities
{
    public partial class SalesShipmentHeader
    {
        [Key]
        public int SalesShipmentHeaderId { get; set; }
        public string SalesShipmentHeaderNo { get; set; }
        public int? DocumentType { get; set; }
        public int? DocStatus { get; set; }
        public string SelltoCustomerNo { get; set; }
        public string BilltoCustomerNo { get; set; }
        public string BilltoName { get; set; }
        public string BilltoName2 { get; set; }
        public string BilltoAddress { get; set; }
        public string BilltoAddress2 { get; set; }
        public string BilltoCity { get; set; }
        public string BilltoContact { get; set; }
        public string YourReference { get; set; }
        public string ShiptoCode { get; set; }
        public string ShiptoName { get; set; }
        public string ShiptoName2 { get; set; }
        public string ShiptoAddress { get; set; }
        public string ShiptoAddress2 { get; set; }
        public string ShiptoCity { get; set; }
        public string ShiptoContact { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? PostingDate { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string PostingDescription { get; set; }
        public string PaymentTermsCode { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? PaymentDiscountPercent { get; set; }
        public DateTime? PmtDiscountDate { get; set; }
        public string ShipmentMethodCode { get; set; }
        public string LocationCode { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string CustomerPostingGroup { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? CurrencyFactor { get; set; }
        public string CustomerPriceGroup { get; set; }
        public byte? PricesIncludingVat { get; set; }
        public string InvoiceDiscCode { get; set; }
        public string CustomerDiscGroup { get; set; }
        public string LanguageCode { get; set; }
        public string SalespersonCode { get; set; }
        public string OrderNo { get; set; }
        public int? NoPrinted { get; set; }
        public string OnHold { get; set; }
        public int? AppliestoDocType { get; set; }
        public string AppliestoDocNo { get; set; }
        public string BalAccountNo { get; set; }
        public string VatregistrationNo { get; set; }
        public string ReasonCode { get; set; }
        public string GenBusPostingGroup { get; set; }
        public byte? Eu3partyTrade { get; set; }
        public string TransactionType { get; set; }
        public string TransportMethod { get; set; }
        public string Vatcountry { get; set; }
        public string SelltoCustomerName { get; set; }
        public string SelltoCustomerName2 { get; set; }
        public string SelltoAddress { get; set; }
        public string SelltoAddress2 { get; set; }
        public string SelltoCity { get; set; }
        public string SelltoContact { get; set; }
        public string BilltoPostCode { get; set; }
        public string BilltoCountry { get; set; }
        public string SelltoPostCode { get; set; }
        public string SelltoCountry { get; set; }
        public string ShiptoPostCode { get; set; }
        public string ShiptoCountry { get; set; }
        public int? BalAccountType { get; set; }
        public string ExitPoint { get; set; }
        public byte? Correction { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string ExternalDocumentNo { get; set; }
        public string Area { get; set; }
        public string TransactionSpecification { get; set; }
        public string PaymentMethodCode { get; set; }
        public string ShippingAgentCode { get; set; }
        public string PackageTrackingNo { get; set; }
        public string NoSeries { get; set; }
        public string OrderNoSeries { get; set; }
        public string UserId { get; set; }
        public string SourceCode { get; set; }
        public string TaxAreaCode { get; set; }
        public byte? TaxLiable { get; set; }
        public string VatbusPostingGroup { get; set; }
        public decimal? VatbaseDiscountPercent { get; set; }
        public string QuoteNo { get; set; }
        public string CampaignNo { get; set; }
        public string SelltoContactNo { get; set; }
        public string BilltoContactNo { get; set; }
        public string ResponsibilityCenter { get; set; }
        public DateTime? RequestedDeliveryDate { get; set; }
        public DateTime? PromisedDeliveryDate { get; set; }
        public string ShippingTime { get; set; }
        public string OutboundWhseHandlingTime { get; set; }
        public string ShippingAgentServiceCode { get; set; }
        public byte? AllowLineDisc { get; set; }
        public short RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
    }
}
