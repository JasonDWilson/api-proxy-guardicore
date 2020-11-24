using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class DeceptionDescriptionTemplateArgs
    {
        public string source_asset_type { get; set; }
        public string source_asset_id{get;set;} 
        public string source_asset_value { get; set; }
        public string destination_asset_type { get; set; }
        public string destination_asset_id { get; set; }
        public string destination_asset_value { get; set; }
        public string hpvm_id { get; set; }
    }
}
