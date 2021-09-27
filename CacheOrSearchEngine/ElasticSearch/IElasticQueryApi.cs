using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CacheOrSearchEngine.ElasticSearch
{
    public interface IElasticQueryApi
    {
        Task CreateIndexAysnc<TDocument>(TDocument document, string indexName);
        Task<List<TDocument>> SearchAysnc<TDocument>(string query, string indexName);
        Task DeleteAsync<TDocument>(string query, string indexName);
        Task DeleteAsync<TDocument>(string[] query, string indexName);
        Task DeleteIndexAsync(string indexName);
        Task DeleteAllIndexAsync();
    }
}
