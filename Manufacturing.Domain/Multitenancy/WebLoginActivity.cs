using System;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Domain.Multitenancy
{
    public partial class WebLoginActivity
    {
        [Key]
        public int Id { get; set; }
        public string IP_Addr { get; set; } = "";
        public string Email { get; set; } = "";
        public string ClientOS { get; set; } = "";
        public string ClientBrowser { get; set; } = "";
        public int? ClientMemory { get; set; } = 0;
        public int? ClientCore { get; set; } = 0;
        public DateTime? LastLoginTime { get; set; }
    }
}
