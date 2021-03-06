using Nest;

namespace CacheOrSearchEngine.ElasticSearch
{
    public class ElasticSearchFactory: IElasticSearchFactory
    {
        private readonly ConfigElastic _configElastic;
        public ElasticSearchFactory(ConfigElastic configElastic)
        {
            _configElastic = configElastic;
        }
        public ElasticClient ElasticClient
        {
            get
            {
                var settings = new ConnectionSettings(_configElastic.GetUri).DisableDirectStreaming(true);
                return new ElasticClient(settings);
            }
        }
    }
}
