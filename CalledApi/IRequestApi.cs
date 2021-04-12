using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalledApi
{
    public interface IRequestApi
    {
        Task<T> SendGetAsync<T>(string apiName, string url, T content, List<KeyValuePair<string, string>> httpHeader);
        Task<T> SendGetAsync<T>(string apiName, string url);
        Task<T> SendPostAsync<T>(string apiName, string url, T content, List<KeyValuePair<string, string>> httpHeader);
        Task<T> SendPostAsync<T>(string apiName, string url, T content);
        Task<T> SendPutAsync<T>(string apiName, string url, T content, List<KeyValuePair<string, string>> httpHeader);
        Task<T> SendPutAsync<T>(string apiName, string url, T content);
        Task<T> SendDeleteAsync<T>(string apiName, string url, T content, List<KeyValuePair<string, string>> httpHeader);
        Task<T> SendDeleteAsync<T>(string apiName, string url);
    }
}
