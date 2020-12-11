namespace Manufacturing.Domain.Multitenancy
{
    public class SystemUserRoles : BaseEntity
    {
        public int SystemUserRolesID { get; set; }
        public string CompanyCode { get; set; }
        public string UserCode { get; set; }
        public string RoleID { get; set; }
        public byte DefaultCompany { get; set; }
        public string LastVersion { get; set; }
    }
}
