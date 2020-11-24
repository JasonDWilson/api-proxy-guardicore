using System;
using System.Linq;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class Incident
    {
        public Asset[] affected_assets { get; set; }
        public int closed_time { get; set; }
        public object[] concatenated_tags { get; set; }
        public object[] description { get; set; }
        public object[] description_template { get; set; }
        public DeceptionDescriptionTemplateArgs description_template_args { get; set; }
        public Asset destination_asset { get; set; }
        public string destination_net { get; set; }
        public string[] destinations { get; set; }
        public string direction{get;set;}
        public int doc_version { get; set; }
        public long end_time { get; set; }
        public bool ended { get; set; }
        public bool enriched { get; set; }
        public Event[] events { get; set; }
        public Guid experimental_id { get; set; }
        public object first_asset { get; set; }
        public string[] flow_ids { get; set; }
        public bool has_export { get; set; }
        public bool has_policy_violations { get; set; }
        public Guid id { get; set; }
        public object[] incident_group { get; set; }
        public string incident_type { get; set; }
        public object[] iocs { get; set; }
        public bool is_experimental { get; set; }
        public string[] labels { get; set; }
        public long last_updated_time { get; set; }
        public Guid originl_id { get; set; }
        public int policy_revision { get; set; }
        public int processed_eventS_count { get; set; }
        public Recommendation[] recommendations { get; set; }
        public int reenrich_count { get; set; }
        public string remote_index { get; set; }
        public Asset second_asset { get; set; }
        public string sensor_name { get; set; }
        public string sensor_type { get; set; }
        public int severity { get; set; }
        public bool similarity_calculated { get; set; }
        public Asset source_asset { get; set; }
        public VM source_vm { get; set; }
        public string source_vm_id { get; set; }
        public string source_ip { get; set; }
        public long start_time { get; set; }
        public int total_events_count { get; set; }
        public string _cls { get; set; }
        public Guid _id { get; set; }
    }
}
