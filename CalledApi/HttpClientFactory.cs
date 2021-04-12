using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CalledApi
{
    public class HttpClientFactory: IHttpClientFactory
    {
        public HttpClient CreateHttpClient(string apiName)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiName);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }
    }
}
