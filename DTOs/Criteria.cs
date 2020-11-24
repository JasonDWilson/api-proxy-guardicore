using System;
using System.Linq;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class Criteria
    {
        public string argument { get; set; }

        public bool case_sensitive { get; set; }

        public string field { get; set; }

        public string op { get; set; }
    }
}