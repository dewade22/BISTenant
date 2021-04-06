﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Data.Entities
{
    public partial class Vendors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorId { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string VendorNo { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string VendorName { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string VendorName2 { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string VendorAddress { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string VendorAddress2 { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string VendorCity { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string VendorContact { get; set; }

        [RegularExpression(@"^([\+][0-9]{1,3}([ \.\-])?)?([\(][0-9]{1,6}[\)])?([0-9 \.\-]{1,32})(([A-Za-z \:]{1,11})?[0-9]{1,4}?)$")]
        [StringLength(20, MinimumLength = 3)]
        public string VendorPhoneNo { get; set; }

        [RegularExpression(@"^([\+][0-9]{1,3}([ \.\-])?)?([\(][0-9]{1,6}[\)])?([0-9 \.\-]{1,32})(([A-Za-z \:]{1,11})?[0-9]{1,4}?)$")]
        [StringLength(20, MinimumLength = 3)]
        public string VendorFaxNo { get; set; }

        public string VendorTelexNo { get; set; }

        public string OurAccountNo { get; set; }

        public string TerritoryCode { get; set; }

        public string GlobalDimension1Code { get; set; }

        public string GlobalDimension2Code { get; set; }

        public decimal? BudgetedAmount { get; set; }

        public string VendorPostingGroup { get; set; }

        public string CurrencyCode { get; set; }

        public string LanguageCode { get; set; }

        public int? StatisticsGroup { get; set; }

        public string PaymentTermsCode { get; set; }

        public string FinChargeTermsCode { get; set; }

        public string PurchaserCode { get; set; }

        public string ShipmentMethodCode { get; set; }

        public string ShippingAgentCode { get; set; }

        public string InvoiceDiscCode { get; set; }

        public string Country { get; set; }

        public int? Blocked { get; set; }

        public string PaytoVendorNo { get; set; }

        public int? Priority { get; set; }

        public string PaymentMethodCode { get; set; }

        public int? ApplicationMethod { get; set; }

        public byte? PricesIncludingVat { get; set; }

        public string TelexAnswerBack { get; set; }

        public string VatregistrationNo { get; set; }

        public string GenBusPostingGroup { get; set; }

        public string MobileNo { get; set; }

        public string PostCode { get; set; }

        public string Email { get; set; }

        public string HomePage { get; set; }

        public string NoSeries { get; set; }

        public string TaxAreaCode { get; set; }

        public byte? TaxLiable { get; set; }

        public string VatbusPostingGroup { get; set; }

        public byte? BlockPaymentTolerance { get; set; }

        public string IcpartnerCode { get; set; }

        public decimal? PrepaymentPercent { get; set; }

        public string PrimaryContactNo { get; set; }

        public string ResponsibilityCenter { get; set; }

        public string LocationCode { get; set; }

        public string LeadTimeCalculation { get; set; }

        public string BaseCalendarCode { get; set; }

        public int? RtcfilterField { get; set; }

        public string BuyerGroupCode { get; set; }

        public string BuyerId { get; set; }

        public short RowStatus { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedTime { get; set; }
    }
    
}