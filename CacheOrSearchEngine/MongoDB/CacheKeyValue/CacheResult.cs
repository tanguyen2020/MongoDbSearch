using MongoDB.Bson;

namespace CacheOrSearchEngine.MongoDB
{
    public class CacheResult
    {
        public string key { get; set; }
        public string Value { get; set; }
        public CacheResult(string key, string value)
        {
            this.key = key;
            this.Value = value;
        }
    }
}
