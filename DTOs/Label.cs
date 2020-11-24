using System;
using System.Linq;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class Label
    {
        public Criteria[] criteria { get; set; }
        public int doc_version { get; set; }
        public Guid id { get; set; }
        public string key { get; set; }
        public string source { get; set; }
        public string value { get; set; }
        public Guid _id { get; set; }
    }
}