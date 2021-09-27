using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CacheOrSearchEngine.ElasticSearch
{
    public class ElasticQueryApi : IElasticQueryApi
    {
        public async Task CreateIndexAysnc<TDocument>(TDocument document, string indexName)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = System.Net.Http.HttpMethod.Post;
            requestMessage.RequestUri = new Uri($"http://localhost:9200/{indexName}");
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(document, Formatting.Indented), UTF8Encoding.UTF8, "application/json");
            await httpClient.SendAsync(requestMessage);
        }

        public async Task DeleteAsync<TDocument>(string query, string indexName)
        {
            MemberInfo[] members = typeof(TDocument).GetMembers();
            var memberInfos = members.Where(p => p.MemberType == MemberTypes.Property).Select(x => x.Name);

            var body = Extenssion.BuildBodyDelete(query, memberInfos);

            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = System.Net.Http.HttpMethod.Post;
            requestMessage.RequestUri = new Uri($"http://localhost:9200/{indexName}/_delete_by_query");
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(body, Formatting.Indented), UTF8Encoding.UTF8, "application/json");
            await httpClient.SendAsync(requestMessage);
        }
        
        public async Task DeleteAsync<TDocument>(string[] query, string indexName)
        {
            MemberInfo[] members = typeof(TDocument).GetMembers();
            var memberInfos = members.Where(p => p.MemberType == MemberTypes.Property).Select(x => x.Name);

            var body = Extenssion.BuildBodyDelete(query, memberInfos);

            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = System.Net.Http.HttpMethod.Post;
            requestMessage.RequestUri = new Uri($"http://localhost:9200/{indexName}/_delete_by_query");
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(body, Formatting.Indented), UTF8Encoding.UTF8, "application/json");
            await httpClient.SendAsync(requestMessage);
        }

        public async Task DeleteIndexAsync(string indexName)
        {
            HttpClient httpClient = new HttpClient();
            await httpClient.DeleteAsync($"http://localhost:9200/{indexName}");
        }

        public async Task DeleteAllIndexAsync()
        {
            HttpClient httpClient = new HttpClient();
            await httpClient.DeleteAsync($"http://localhost:9200/_all");
        }

        public async Task<List<TDocument>> SearchAysnc<TDocument>(string query, string indexName)
        {
            MemberInfo[] members = typeof(TDocument).GetMembers();
            var memberInfos = members.Where(p => p.MemberType == MemberTypes.Property).Select(x => x.Name);

            var body = Extenssion.BuildBodySearch(query, memberInfos);

            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = System.Net.Http.HttpMethod.Get;
            requestMessage.RequestUri = new Uri($"http://localhost:9200/{indexName}/_search");
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(body, Formatting.Indented), UTF8Encoding.UTF8, "application/json");
            var result = await httpClient.SendAsync(requestMessage);
            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                var resultContentJson = (JObject)JsonConvert.DeserializeObject(resultContent);
                var content = resultContentJson["hits"].ToObject<Hits<TDocument>>();
                return content.Hist.Select(x => x.Source).ToList();
            }
            return null;
        }
    }
}
