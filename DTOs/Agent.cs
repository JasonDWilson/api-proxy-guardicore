using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class Agent
    {
        public string _id { get; set; }

        public string aggregator_component_id { get; set; }

        public string asset_id { get; set; }

        public string component_id { get; set; }

        public string display_status { get; set; }

        public int doc_version { get; set; }

        public int dropped_events_processed { get; set; }

        public long first_seen { get; set; }

        public object health { get; set; }

        public string hostname { get; set; }

        public string id { get; set; }

        public string[] ip_addresses { get; set; }

        public bool is_aggregator { get; set; }

        public bool is_configuration_dirty { get; set; }

        public bool is_missing { get; set; }

        public Label[] labels { get; set; }

        public long last_received_flows_time { get; set; }

        public long last_seen { get; set; }

        public OperatingSystemType os { get; set; }

        public object policy_revision { get; set; }

        public long reported_connections_processed { get; set; }

        public object[] status_flags { get; set; }

        public string version { get; set; }
    }
}
