using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CalledApi
{
    public interface IHttpClientFactory
    {
        HttpClient CreateHttpClient(string apiName);
    }
}
