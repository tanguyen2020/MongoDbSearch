using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Cosmos
{
    public class CacheResult
    {
        [JsonProperty("id")]
        public string key { get; }
        public string Value { get; }

        // Summary: 
        // used to set expiration policy
        // The system will automatically delete items based on the TTL value(in seconds) you provide, without needing a delete operation explicitly issued by a client application
        [JsonProperty(PropertyName = "ttl", NullValueHandling = NullValueHandling.Ignore)]
        public int TimeToLive { get; set; } = -1;

        public CacheResult(string key, string value, int expiry = -1)
        {
            this.key = key;
            this.Value = value;
            TimeToLive = expiry == 0 ? -1 : expiry;
        }
    }
}
