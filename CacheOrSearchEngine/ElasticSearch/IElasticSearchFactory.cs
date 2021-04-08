using Nest;
using System;

namespace CacheOrSearchEngine.ElasticSearch
{
    public interface IElasticSearchFactory
    {
        ElasticClient ElasticClient { get; }
    }
}
