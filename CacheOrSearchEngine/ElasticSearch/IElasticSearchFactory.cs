using Nest;

namespace CacheOrSearchEngine.ElasticSearch
{
    public interface IElasticSearchFactory
    {
        ElasticClient ElasticClient { get; }
    }
}
