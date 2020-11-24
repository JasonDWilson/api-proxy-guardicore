using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class AssetID
    {
        private string _id;
        public AssetID(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("must supply id");

            _id = $"vm:{id}";
        }

        public override string ToString()
        {
            return _id;
        }
    }
}
