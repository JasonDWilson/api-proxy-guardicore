using System;
using System.Linq;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class Recommendation
    {
        public Guid id { get; set; }
        public bool action_required { get; set; }
        public object[] parts { get; set; }
        public string handle_template { get; set; }
        public string[] optional_actions { get; set; }
        public object[] details { get; set; }
        public string type { get; set; }
        public Guid rule_id { get; set; }
        public string rule_type { get; set; }
    }
}