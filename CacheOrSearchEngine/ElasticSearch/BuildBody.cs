using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CacheOrSearchEngine.ElasticSearch
{
    [Serializable]
    public class BuildBody
    {
        [JsonProperty("size")]
        public int Size { get; set; } = 50;
        [JsonProperty("sort")]
        public List<Sort> Sort { get; set; }
        [JsonProperty("query")]
        public Query Query { get; set; }
    }

    [Serializable]
    public class BuildBodyDelete
    {
        public List<Sort> Sort { get; set; }
        [JsonProperty("query")]
        public Query Query { get; set; }
    }

    public class Sort
    {
        [JsonProperty("_score")]
        public OrderBy Score { get; set; }
    }

    public class OrderBy
    {
        [JsonProperty("order")]
        public string Order { get; set; } = "desc";
    }

    public class Query
    {
        [JsonProperty("bool")]
        public BoolSearch Bool { get; set; }
    }

    public class BoolSearch
    {
        [JsonProperty("must")]
        public List<Must> Must { get; set; }
    }

    public class Must
    {
        [JsonProperty("multi_match")]
        public MultiMatch MultiMatch { get; set; }
    }

    public class MultiMatch
    {
        [JsonProperty("query")]
        public string Query { get; set; }
        [JsonProperty("fields")]
        public object[] Fields { get; set; }
        [JsonProperty("type")]
        public string TypeSearch { get; set; }
    }

    [Serializable]
    public class Hits<TDocument>
    {
        [JsonProperty("hits")]
        public List<HitsSeconds<TDocument>> Hist { get; set; }
    }
    public class HitsSeconds<TDocument>
    {
        [JsonProperty("_source")]
        public TDocument Source { get; set; }
    }
}
