using System;
using System.Linq;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class Nic
    {
        public string[] discovered_ip_addresses { get; set; }
        public string[] ip_addresses { get; set; }
        public string mac_address { get; set; }
        public Guid network_id { get; set; }
        public string network_name { get; set; }
        public string network_orchestration_id { get; set; }
        public string orchestration_driver_id { get; set; }
        public string orchestration_driver_type { get; set; }
        public string switch_id { get; set; }
        public string vif_id { get; set; }
        public int vlan_id { get; set; }
    }
}
