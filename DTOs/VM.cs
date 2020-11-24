using System;
using System.Linq;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class VM
    {
        public string full_name { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public Nic[] nics { get; set; }
        public string orchestration_driver_id { get; set; }
        public string orchestration_driver_type { get; set; }
        public string[] recent_domains { get; set; }
        public string tenant_name{get;set;}
    }
}