using BaseObject.DataObject;
using Nest;

namespace CacheOrSearchEngine.ElasticSearch
{
    public interface IElasticQuery: IElasticQueryAsync
    {
        /// <summary>
        /// Create Index data for ElasticSearch
        /// </summary>
        /// <param name="objData"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        bool CreateIndex(object objData, string indexName);

        /// <summary>
        /// Search data cache ElasticSearch with indexName and string text
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="indexName"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        ISearchResponse<DataObject> Search(string strText, string indexName, int size = 10);

        /// <summary>
        /// Delete for index ElasticSearch
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        bool Delete(string indexName);

        /// <summary>
        /// Delete all index ElasticSearch
        /// </summary>
        /// <returns></returns>
        bool DeleteAllIndex();
    }
}
