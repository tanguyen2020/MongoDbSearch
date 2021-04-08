using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CacheOrSearchEngine.ElasticSearch
{
    public interface IElasticQueryAsync
    {
        /// <summary>
        /// Create Index data for ElasticSearch
        /// </summary>
        /// <param name="objData"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        Task<bool> CreateIndexAsync(object objData, string indexName);

        /// <summary>
        /// Search data cache ElasticSearch with indexName and string text
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="indexName"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<ISearchResponse<ElasticModel>> SearchAsync(string strText, string indexName, int size = 10);

        /// <summary>
        /// Delete for index ElasticSearch
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string indexName);

        /// <summary>
        /// Delete all index ElasticSearch
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteAllIndexAsync();
    }
}
