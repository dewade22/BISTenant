using System;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Domain.Multitenancy
{
    public partial class Client : BaseEntity
    {
        [Key]
        public string CompanyCode { get; set; } // varchar(20), not null
        public string ParentCompanyCode { get; set; } // varchar(20), null
        public string CompanyName { get; set; } // varchar(50), not null
        public string CompanyName2 { get; set; } // varchar(50), null
        public string CompanyAddress { get; set; } // varchar(80), not null
        public string CompanyAddress2 { get; set; } // varchar(80), null
        public string CompanyCity { get; set; } // varchar(30), null
        public string CompanyPhoneNo { get; set; } // varchar(30), null
        public string CompanyPhoneNo2 { get; set; } // varchar(30), null
        public string CompanyFaxNo { get; set; } // varchar(30), null
        public string GiroNo { get; set; } // varchar(20), null
        public string BankName { get; set; } // varchar(50), null
        public string BankBranchNo { get; set; } // varchar(50), null
        public string BankAccountNo { get; set; } // varchar(30), null
        public string BankAccountName { get; set; } // varchar(50), null
        public string BankAddress { get; set; } // varchar(80), null
        public string PaymentRoutingNo { get; set; } // varchar(20), null
        public string CustomsPermitNo { get; set; } // varchar(20), null
        public DateTime? CustomsPermitDate { get; set; } // datetime, null
        public string VATRegistrationNo { get; set; } // varchar(20), null
        public string RegistrationNo { get; set; } // varchar(20), null
        public string ShiptoName { get; set; } // varchar(50), null
        public string ShiptoName2 { get; set; } // varchar(50), null
        public string ShiptoAddress { get; set; } // varchar(50), null
        public string ShiptoAddress2 { get; set; } // varchar(50), null
        public string ShiptoCity { get; set; } // varchar(30), null
        public string ShiptoContact { get; set; } // varchar(50), null
        public string LocationCode { get; set; } // varchar(10), null
        public string ImageFolderName { get; set; } // varchar(100), null
        public string PostCode { get; set; } // varchar(20), null
        public string ShiptoPostCode { get; set; } // varchar(20), null
        public string EMail { get; set; } // varchar(80), null
        public string HomePage { get; set; } // varchar(80), null
        public string CountryRegionCode { get; set; } // varchar(10), null
        public string ShiptoCountryRegionCode { get; set; } // varchar(10), null
        public string IBAN { get; set; } // varchar(50), null
        public string SWIFTCode { get; set; } // varchar(20), null
        public string IndustrialClassification { get; set; } // varchar(30), null
        public string AbbreviatedName { get; set; } // varchar(4), null
        public byte? ShowAbbreviatedName { get; set; } // tinyint, null
        public int? SystemIndicator { get; set; } // int, null
        public string CustomSystemIndicatorText { get; set; } // varchar(250), null
        public int? SystemIndicatorStyle { get; set; } // int, null
        public string ResponsibilityCenter { get; set; } // varchar(10), null
        public string CheckAvailPeriodCalc { get; set; } // varchar(32), null
        public int? CheckAvailTimeBucket { get; set; } // int, null
        public string BaseCalendarCode { get; set; } // varchar(10), null
        public string CalConvergenceTimeFrame { get; set; } // varchar(32), null
        public string ABN { get; set; } // varchar(11), null
        public int? TaxPeriod { get; set; } // int, null
        public string ABNDivisionPartNo { get; set; } // varchar(3), null
        public string IRDNo { get; set; } // varchar(30), null
        public string RDOCode { get; set; } // varchar(3), null
        public DateTime? VATRegistrationDate { get; set; } // datetime, null
        public string SignInvoiceName { get; set; } // varchar(50), null
        public string SignInvoiceDept { get; set; } // varchar(50), null
        public string APPVersion { get; set; } // varchar(50), null
        public string FileFolder { get; set; } // varchar(250), null
        public string LoginImageName { get; set; } // varchar(250), null
        public string LogoFileName { get; set; } // varchar(250), null
        public string WallFileName { get; set; } // varchar(250), null
        public string SecurityCheck { get; set; } // varchar(250), null

        public string ConnectionString { get; set; } // varchar(250), null

        public Client()
        {
            this.CompanyCode = "";
            this.ParentCompanyCode = "";
            this.CompanyName = "";
            this.CompanyName2 = "";
            this.CompanyAddress = "";
            this.CompanyAddress2 = "";
            this.CompanyCity = "";
            this.CompanyPhoneNo = "";
            this.CompanyPhoneNo2 = "";
            this.CompanyFaxNo = "";
            this.GiroNo = "";
            this.BankName = "";
            this.BankBranchNo = "";
            this.BankAccountNo = "";
            this.BankAccountName = "";
            this.BankAddress = "";
            this.PaymentRoutingNo = "";
            this.CustomsPermitNo = "";
            this.CustomsPermitDate = null;
            this.VATRegistrationNo = "";
            this.RegistrationNo = "";
            this.ShiptoName = "";
            this.ShiptoName2 = "";
            this.ShiptoAddress = "";
            this.ShiptoAddress2 = "";
            this.ShiptoCity = "";
            this.ShiptoContact = "";
            this.LocationCode = "";
            this.ImageFolderName = "";
            this.PostCode = "";
            this.ShiptoPostCode = "";
            this.EMail = "";
            this.HomePage = "";
            this.CountryRegionCode = "";
            this.ShiptoCountryRegionCode = "";
            this.IBAN = "";
            this.SWIFTCode = "";
            this.IndustrialClassification = "";
            this.AbbreviatedName = "";
            this.ShowAbbreviatedName = 0;
            this.SystemIndicator = 0;
            this.CustomSystemIndicatorText = "";
            this.SystemIndicatorStyle = 0;
            this.ResponsibilityCenter = "";
            this.CheckAvailPeriodCalc = "";
            this.CheckAvailTimeBucket = 0;
            this.BaseCalendarCode = "";
            this.CalConvergenceTimeFrame = "";
            this.ABN = "";
            this.TaxPeriod = 0;
            this.ABNDivisionPartNo = "";
            this.IRDNo = "";
            this.RDOCode = "";
            this.VATRegistrationDate = null;
            this.SignInvoiceName = "";
            this.SignInvoiceDept = "";
            this.APPVersion = "";
            this.FileFolder = "";
            this.LoginImageName = "";
            this.LogoFileName = "";
            this.WallFileName = "";
            this.SecurityCheck = "";
        }
    }
}
