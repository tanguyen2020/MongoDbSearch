using BaseObject.DataObject;
using Nest;
using System.Threading.Tasks;

namespace CacheOrSearchEngine.ElasticSearch
{
    public class ElasticQuery: IElasticQuery
    {
        private IElasticSearchFactory _elasticSearchFactory;
        public ElasticQuery(IElasticSearchFactory elasticSearchFactory)
        {
            _elasticSearchFactory = elasticSearchFactory;
        }

        /// <summary>
        /// Create Index data for ElasticSearch
        /// </summary>
        /// <param name="objData"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<bool> CreateIndexAsync(object objData, string indexName)
        {
            var index = await _elasticSearchFactory.ElasticClient.IndexAsync(objData, i => i.Index(indexName));
            if (index.IsValid) return true;
            return false;
        }

        /// <summary>
        /// Search data cache ElasticSearch with indexName and string text
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="indexName"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<ISearchResponse<DataObject>> SearchAsync(string strText, string indexName, int size = 10)
        {
            return await _elasticSearchFactory.ElasticClient.SearchAsync<DataObject>(
                s => s.Index(indexName)
                .Size(size)
                .Query(q => q.MatchPhrasePrefix(m => m.Field(f => f["Name"]).Query(strText)))
                .Sort(p => p.Descending(SortSpecialField.DocumentIndexOrder)));
        }

        /// <summary>
        /// Delete for index ElasticSearch
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string indexName)
        {
            var delete = await _elasticSearchFactory.ElasticClient.DeleteByQueryAsync<ElasticModel>(i => i.Index(indexName).MatchAll());
            if (delete.IsValid) return true;
            return false;
        }

        /// <summary>
        /// Delete all index ElasticSearch
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteAllIndexAsync()
        {
            var delete = await _elasticSearchFactory.ElasticClient.DeleteByQueryAsync<ElasticModel>(i => i.AllIndices().MatchAll());
            if (delete.IsValid) return true;
            return false;
        }

        /// <summary>
        /// Create Index data for ElasticSearch
        /// </summary>
        /// <param name="objData"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public bool CreateIndex(object objData, string indexName) => CreateIndexAsync(objData, indexName).Result;

        /// <summary>
        /// Search data cache ElasticSearch with indexName and string text
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="indexName"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public ISearchResponse<DataObject> Search(string strText, string indexName, int size = 10) => SearchAsync(strText, indexName, size).Result;

        /// <summary>
        /// Delete for index ElasticSearch
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public bool Delete(string indexName) => DeleteAsync(indexName).Result;

        /// <summary>
        /// Delete all index ElasticSearch
        /// </summary>
        /// <returns></returns>
        public bool DeleteAllIndex() => DeleteAllIndexAsync().Result;
    }

    public class ElasticModel
    {
        public string Name { get; set; }
    }
}
