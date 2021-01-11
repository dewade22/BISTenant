namespace Manufacturing.Domain.Multitenancy
{
    public class SystemPermission : BaseEntity
    {
        public int SystemPermissionID { get; set; }
        public string CompanyCode { get; set; }
        public string RoleID { get; set; }
        public string ObjectID { get; set; }
        public byte ReadData { get; set; }
        public byte InsertData { get; set; }
        public byte ModifyData { get; set; }
        public byte DeleteData { get; set; }
        public byte ExecuteData { get; set; }
        public byte ValueData { get; set; }
    }
}
