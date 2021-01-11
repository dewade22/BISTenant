namespace Manufacturing.Domain.Multitenancy
{
    public class SystemObject : BaseEntity
    {
        public int SystemObjectID { get; set; }
        public string ObjectType { get; set; }
        public string ObjectDesc { get; set; }
        public string ObjectSystemName { get; set; }
        public string ObjectSystemArg { get; set; }
        public string ObjectSystemImageFileName { get; set; }
        public string UrlObjectName { get; set; }
        public byte? ObjectSeqNo { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public SystemObject()
        {
            this.SystemObjectID = 0;
            this.ObjectType = "";
            this.ObjectDesc = "";
            this.ObjectSystemName = "";
            this.ObjectSystemArg = "";
            this.ObjectSystemImageFileName = "";
            this.UrlObjectName = "";
            this.ObjectSeqNo = 0;
            this.ControllerName = "";
            this.ActionName = "";
        }
    }
}
