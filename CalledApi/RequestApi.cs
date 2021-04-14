using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CalledApi
{
    public class RequestApi : IRequestApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RequestApi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<T> SendGetAsync<T>(string apiName, string url, HttpContent content, List<KeyValuePair<string, string>> httpHeader)
        {
            return await Send<T>(apiName, url, content, httpHeader, HttpMethod.Get);
        }

        public async Task<T> SendGetAsync<T>(string apiName, string url)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateHttpClient(apiName);
                var result = await httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);

                return default(T);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> SendPostAsync<T>(string apiName, string url, HttpContent content, List<KeyValuePair<string, string>> httpHeader)
        {
            return await Send<T>(apiName, url, content, httpHeader, HttpMethod.Post);
        }

        public async Task<T> SendPostAsync<T>(string apiName, string url, HttpContent content)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateHttpClient(apiName);
                var result = await httpClient.PostAsync(url, content);
                if (result.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);

                return default(T);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> SendPutAsync<T>(string apiName, string url, HttpContent content, List<KeyValuePair<string, string>> httpHeader)
        {
            return await Send<T>(apiName, url, content, httpHeader, HttpMethod.Put);
        }

        public async Task<T> SendPutAsync<T>(string apiName, string url, HttpContent content)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateHttpClient(apiName);
                var result = await httpClient.PutAsync(url, content);
                if (result.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                return default(T);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> SendDeleteAsync<T>(string apiName, string url, HttpContent content, List<KeyValuePair<string, string>> httpHeader)
        {
            return await Send<T>(apiName, url, content, httpHeader, HttpMethod.Delete);
        }

        public async Task<T> SendDeleteAsync<T>(string apiName, string url)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateHttpClient(apiName);
                var result = await httpClient.DeleteAsync(url);
                if (result.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                return default(T);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> Send<T>(string apiName, string url, HttpContent content, List<KeyValuePair<string, string>> httpHeader, HttpMethod method)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateHttpClient(apiName);
                var httpRequest = new HttpRequestMessage(method, url);
                httpRequest.Content = content;
                var accessToken = GenerateToken("");
                httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                if(httpHeader != null)
                {
                    foreach (var item in httpHeader)
                    {
                        httpRequest.Headers.Add(item.Key, item.Value);
                    }
                }
                var result = await httpClient.SendAsync(httpRequest);
                if (result.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());

                return default(T);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GenerateToken(string audId, int? userId = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(audId));
            var tokenHanlder = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Audience = audId,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHanlder.WriteToken(tokenHanlder.CreateToken(tokenDescriptor));
        }
    }
}
