using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Domain.Multitenancy
{
    public class SystemUserMenu : BaseEntity
    {
        public int SystemUserMenuID { get; set; }
        public string CompanyCode { get; set; }
        public string RoleID { get; set; }
        public int ParentID { get; set; }
        public int ChildID { get; set; }
        public int SeqNo { get; set; }

        [ForeignKey("ParentID")]
        public SystemObject ParentObject { get; set; }

        [ForeignKey("ChildID")]
        public SystemObject ChildObject { get; set; }

    }
}
