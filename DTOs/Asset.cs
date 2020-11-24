using System;
using System.Linq;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class Asset
    {
        public string country { get; set; }
        public string country_code { get; set; }
        public string ip { get; set; }
        public bool is_inner { get; set; }
        public string[] labels { get; set; }
        public VM vm { get; set; }
        public string vm_id { get; set; }
    }
}
