using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class PaginatedResponse<T>
    {
        public int current_page { get; set; }
        public string db_query_time { get; set; }
        public string dict_mapping_time { get; set; }
        public object filters { get; set; }
        public long from { get; set; }
        public long to { get; set; }
        public bool is_count_exact { get; set; }
        public string objects_mapping_time { get; set; }
        public int results_in_page { get; set; }
        public int total_count { get; set; }
        public string[] sort { get; set; }
        public List<T> objects { get; set; }
    }
}
