using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class Event
    {
        public string description { get; set; }
        public string[][] destinations { get; set; }
        public int doc_version { get; set; }
        public string event_source { get; set; }
        public string event_type { get; set; }
        public Guid id { get; set; }
        public Guid incident_id { get; set; }
        public bool is_experimental { get; set; }
        public long processed_time { get; set; }
        public long received_time { get; set; }
        public int severity { get; set; }
        public string source_ip { get; set; }
        public VM source_vm { get; set; }
        public string[] tag_refs { get; set; }
        public long time { get; set; }
        public Guid uuid { get; set; }
        public Guid _id { get; set; }
    }
}
