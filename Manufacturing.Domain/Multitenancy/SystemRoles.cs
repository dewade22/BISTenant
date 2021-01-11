namespace Manufacturing.Domain.Multitenancy
{
    public class SystemRoles : BaseEntity
    {
        public int SystemRolesID { get; set; }
        public string Role { get; set; }
        public string Descriptions { get; set; }
    }
}
