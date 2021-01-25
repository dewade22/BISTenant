using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Manufacturing.Domain.Multitenancy
{
    public partial class SystemUsers : BaseEntity
    {
        public int SystemUsersID { get; set; }

        public string UserCode { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public string UserPassword { get; set; }
        public DateTime? AllowPostingFrom { get; set; }
        public DateTime? AllowPostingTo { get; set; }
        public byte RegisterTime { get; set; }
        public string SalespersPurchCode { get; set; }
        public string ApproverID { get; set; }
        public int SalesAmountApprovalLimit { get; set; }
        public int PurchaseAmountApprovalLimit { get; set; }
        public byte UnlimitedSalesApproval { get; set; }
        public byte UnlimitedPurchaseApproval { get; set; }


        public string Substitute { get; set; }
        public string EMailAddress { get; set; }
        public int RequestAmountApprovalLimit { get; set; }
        public byte UnlimitedRequestApproval { get; set; }
        public DateTime? AllowFAPostingFrom { get; set; }
        public DateTime? AllowFAPostingTo { get; set; }
        public string SalesRespCtrFilter { get; set; }
        public string PurchaseRespCtrFilter { get; set; }
        public string ServiceRespCtr_Filter { get; set; }
        public string DepartmentCode { get; set; }
        public string LocationCode { get; set; }

        //public string CompanyCode { get; set; }
        //public string CompanyName { get; set; }
        //public DateTime ExpireDate { get; set; }
        //public string Role { get; set; }

        public SystemUsers()
        {
            this.SystemUsersID = 1;

            this.UserCode = "";
            this.UserName = "";
            this.UserPassword = "";
            this.AllowPostingFrom = null;
            this.AllowPostingTo = null;
            this.RegisterTime = 1;
            this.SalespersPurchCode = "";
            this.ApproverID = "";
            this.SalesAmountApprovalLimit = 1;
            this.PurchaseAmountApprovalLimit = 1;
            this.UnlimitedSalesApproval = 1;
            this.UnlimitedPurchaseApproval = 1;
            this.Substitute = "";
            this.EMailAddress = "";
            this.RequestAmountApprovalLimit = 1;
            this.UnlimitedRequestApproval = 1;
            this.AllowFAPostingFrom = null;
            this.AllowFAPostingTo = null;
            this.SalesRespCtrFilter = "";
            this.PurchaseRespCtrFilter = "";
            this.ServiceRespCtr_Filter = "";
            this.DepartmentCode = "";
            this.LocationCode = "";
        }
    }
}
