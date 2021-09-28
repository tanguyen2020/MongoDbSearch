using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Caching.Core;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.Linq;
//using Microsoft.ApplicationInsights;

namespace Caching.Cosmos
{
    public class CosmosCache : ICosmosCache, IDisposable
    {
        private bool disposed = false;
        private readonly ICacheConfiguration _configuration;
        private DocumentClient documentClient;
        //private TelemetryClient telemetryClient;
        public CosmosCache(ICacheConfiguration configuration)
        {
            _configuration = configuration;
            //telemetryClient = new TelemetryClient();
            ReadCosmos();
        }

        private void ReadCosmos()
        {
            try
            {
                documentClient = new DocumentClient(new Uri(_configuration.CosmosAccountEndpoint), _configuration.CosmosAccountKey);
                var dbCosmos = documentClient.ReadDatabaseFeedAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                var dbCosmosExists = dbCosmos.Where(x => x.Id == _configuration.CosmosDataBaseName).FirstOrDefault();
                if (dbCosmosExists == null)
                {
                    //telemetryClient.TrackException(new Exception($"Database {_configuration.CosmosDataBaseName} not exists on Azure Cosmos."));
                }
                else
                {
                    var colCosmos = documentClient.ReadDocumentCollectionFeedAsync(dbCosmosExists.CollectionsLink).ConfigureAwait(false).GetAwaiter().GetResult();
                    var colCosmosExists = colCosmos.Where(x => x.Id == _configuration.CosmosCollection).FirstOrDefault();
                    if (colCosmosExists == null)
                    {
                        //telemetryClient.TrackException(new Exception($"Container {_configuration.CosmosCollection} not exists on Azure Cosmos."));
                    }
                }
            }
            catch (Exception e)
            {
                //telemetryClient.TrackException(e);
            }
        }

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// Returns: True if the keys were set, else False
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CacheAdd<T>(string key, T value, int timeExpired = -1)
        {
            try
            {
                if (_configuration.EnableCache && (value != null))
                {
                    if (CacheKeyExists(key)) CacheRemove(key);

                    documentClient.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(_configuration.CosmosDataBaseName, _configuration.CosmosCollection), CreateDocument(key, value, timeExpired))
                        .ConfigureAwait(false).GetAwaiter().GetResult();
                    return true;
                }
                return false;
            }
            catch (DocumentClientException ex)
            {
                //telemetryClient.TrackException(ex);
                return false;
            }
        }

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// KeyValuePair<string, T>[]: string is key, T is value below cache
        /// Returns: True if the keys were set, else False
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CacheAdd<T>(KeyValuePair<string, T>[] values)
        {
            if (!values.Any() || !_configuration.EnableCache) return false;
            foreach (var item in values)
            {
                CacheAdd<T>(item.Key, item.Value);
            }
            return true;
        }

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// KeyValuePair<string, ValueTuple<T, int>>:
        /// string is key blow cache
        /// ValueTuple<T, int>: T is value and int is TimeExpired Cache
        /// Returns: True if the keys were set, else False
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CacheAdd<T>(KeyValuePair<string, ValueTuple<T, int>>[] values)
        {
            if (!values.Any() || !_configuration.EnableCache) return false;
            foreach (var item in values)
            {
                CacheAdd<T>(item.Key, item.Value.Item1, item.Value.Item2);
            }
            return true;
        }

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// Returns: True if the keys were set, else False
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CacheSet<T>(string key, T value, int timeExpired = -1) => CacheAdd(key, value, timeExpired);

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// KeyValuePair<string, T>[]: string is key blow cache, T is value below cache
        /// Returns: True if the keys were set, else False
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public bool CacheSet<T>(KeyValuePair<string, T>[] values) => CacheAdd(values);

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// KeyValuePair<string, ValueTuple<T, int>>: string is key blow cache, ValueTuple<T, int>: T is value and int is TimeExpired Cache
        /// Returns: True if the keys were set, else False
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public bool CacheSet<T>(KeyValuePair<string, ValueTuple<T, int>>[] values) => CacheAdd(values);

        /// <summary>
        /// Get the value of key. If the key does not exist the special value null is returned.
        /// Returns: the value of key, or null when key does not exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T CacheGet<T>(string key)
        {
            if (_configuration.EnableCache)
            {
                if (key == null) throw new ArgumentNullException(nameof(key));
                if (CacheKeyExists(key))
                {
                    return GetDocument<T>(key).ConfigureAwait(false).GetAwaiter().GetResult();
                }
            }
            return default(T);
        }

        /// <summary>
        /// Get List value the contains key
        /// </summary>
        /// <param name="containskey"></param>
        /// <param name="draft"></param>
        /// <returns></returns>
        public List<string> CacheGetAllKey()
        {
            if (_configuration.EnableCache)
            {
                var keys = new List<string>();
                var doc = documentClient.CreateDocumentQuery<Document>(UriFactory.CreateDocumentCollectionUri(_configuration.CosmosDataBaseName, _configuration.CosmosCollection)
                    , "select * from c", new FeedOptions { EnableCrossPartitionQuery = true }).AsEnumerable();

                keys.AddRange(doc.Select(p => p.Id));
                return keys;
            }
            return null;
        }

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// Returns: True if the key was removed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CacheRemove(string key)
        {
            if (_configuration.EnableCache)
            {
                if (!CacheKeyExists(key)) return false;
                documentClient.DeleteDocumentAsync(GetDocumentUri(key), RequestOptions())
                     .ConfigureAwait(false).GetAwaiter().GetResult();
                return true;
            }
            return false;

        }

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// Input is Array keys
        /// Returns: The number of keys that were removed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public long CacheRemove(string[] keys)
        {
            if (_configuration.EnableCache)
            {
                if (!keys.Any()) return 0;
                int deleted = 1;
                foreach (var i in keys)
                {
                    if (CacheKeyExists(i))
                    {
                        CacheRemove(i);
                        deleted++;
                    }
                }
                return (deleted - 1);
            }
            return 0;
        }

        /// <summary>
        /// Clear the cache with the contains key if it exists
        /// Query from cache with contains key later delete all cache from the above query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// Example: input_key = "2021-01-01" will query all cache with contains key input_key (select * from c where contains (c.id,input_key)) after will delete all keys with query
        /// <returns></returns>
        public long CacheRemoveContainsKey(string key)
        {
            if (_configuration.EnableCache)
            {
                var listKeys = documentClient.CreateDocumentQuery<CacheResult>(UriFactory.CreateDocumentCollectionUri(_configuration.CosmosDataBaseName, _configuration.CosmosCollection)
                    , new FeedOptions { EnableCrossPartitionQuery = true }).Where(p => p.key.Contains(key)).ToList();
                if (listKeys.Any())
                {
                    int deleted = 1;
                    foreach (var item in listKeys)
                    {
                        CacheRemove(item.key);
                        deleted++;
                    }
                    return (deleted - 1);
                }
            }
            return 0;
        }

        /// <summary>
        /// Returns if key exists.
        /// Returns: 1 if the key exists. 0 if the key does not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CacheKeyExists(string key)
        {
            if (!_configuration.EnableCache) return false;
            try
            {
                var requestOptions = new RequestOptions() { PartitionKey = new PartitionKey(Undefined.Value) };
                documentClient.ReadDocumentAsync(GetDocumentUri(key), RequestOptions()).ConfigureAwait(false).GetAwaiter().GetResult();
                return true;
            }
            catch (Exception e)
            {
                //telemetryClient.TrackException(e);
                return false;
            }
        }

        /// <summary>
        /// Update time expired cache
        /// If present and the timeExpired is set to "-1", it is equal to infinity, and items don’t expire by default.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeExpired"></param>
        /// <returns></returns>
        public bool CacheUpdateInterval(string key, int timeExpired = -1)
        {
            if (_configuration.EnableCache)
            {
                if (!CacheKeyExists(key)) return false;

                Document doc = documentClient.CreateDocumentQuery<Document>(UriFactory.CreateDocumentCollectionUri(_configuration.CosmosDataBaseName, _configuration.CosmosCollection)
                    , new FeedOptions { EnableCrossPartitionQuery = true }).Where(p => p.Id == key).AsEnumerable().SingleOrDefault();
                doc.SetPropertyValue("ttl", timeExpired == 0 ? -1 : timeExpired);
                documentClient.ReplaceDocumentAsync(doc).ConfigureAwait(false).GetAwaiter().GetResult();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update time expired cache
        /// If present and the timeExpired is set to "-1", it is equal to infinity, and items don’t expire by default.
        /// KeyValuePair<string, int>: key is blow cache and int is TimeExpired Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeExpired"></param>
        /// <returns></returns>
        public bool CacheUpdateInterval<T>(KeyValuePair<string, int>[] values)
        {
            if (!_configuration.EnableCache || !values.Any()) return false;
            foreach (var item in values)
            {
                CacheUpdateInterval(item.Key, item.Value);
            }
            return true;
        }

        /// <summary>
        /// Update the list key time expired cache
        /// If present and the timeExpired is set to "-1", it is equal to infinity, and items don’t expire by default.
        /// </summary>
        /// <param name="containsKey"></param>
        /// <param name="timeExpired"></param>
        /// <returns></returns>
        public bool CacheUpdateListKeyInterval(string containsKey, int timeExpired = -1)
        {
            if (_configuration.EnableCache)
            {
                var listKeys = documentClient.CreateDocumentQuery<Document>(UriFactory.CreateDocumentCollectionUri(_configuration.CosmosDataBaseName, _configuration.CosmosCollection)
                       , new FeedOptions { EnableCrossPartitionQuery = true }).Where(p => p.Id.Contains(containsKey)).ToList();
                if (listKeys.Any())
                {
                    foreach (var item in listKeys)
                    {
                        CacheUpdateInterval(item.Id, timeExpired);
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Update value the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// Returns: True if the keys were set, else False
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CacheUpdateValue<T>(string key, T value, int timeExpired = -1)
        {
            if (_configuration.EnableCache)
            {
                if (!CacheKeyExists(key)) return false;

                Document doc = documentClient.CreateDocumentQuery<Document>(UriFactory.CreateDocumentCollectionUri(_configuration.CosmosDataBaseName, _configuration.CosmosCollection)
                    , new FeedOptions { EnableCrossPartitionQuery = true }).Where(p => p.Id == key).AsEnumerable().SingleOrDefault();
                doc.SetPropertyValue("Value", JsonConvert.SerializeObject(value));
                doc.SetPropertyValue("ttl", timeExpired == 0 ? -1 : timeExpired);
                documentClient.ReplaceDocumentAsync(doc).ConfigureAwait(false).GetAwaiter().GetResult();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update value the given keys to their respective values.
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// KeyValuePair<string, T>[]: string is key blow cache, T is value below cache
        /// Returns: True if the keys were set, else False
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CacheUpdateValue<T>(KeyValuePair<string, T>[] values)
        {
            if (_configuration.EnableCache)
            {
                if (!values.Any()) return false;
                foreach (var item in values)
                {
                    CacheUpdateValue<T>(item.Key, item.Value);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update value the given keys to their respective values.
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// KeyValuePair<string, ValueTuple<T, int>>: string is key blow cache, T is value below cache, int is TimeExpired Cache
        /// Returns: True if the keys were set, else False
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CacheUpdateValue<T>(KeyValuePair<string, ValueTuple<T, int>>[] values)
        {
            if (_configuration.EnableCache)
            {
                if (!values.Any()) return false;
                foreach (var item in values)
                {
                    CacheUpdateValue<T>(item.Key, item.Value.Item1, item.Value.Item2);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update value for cache of the List Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public bool CacheUpdateListValue(List<CacheResult> results)
        {
            if (_configuration.EnableCache)
            {
                foreach (var item in results)
                {
                    CacheUpdateValue<object>(item.key, item.Value, item.TimeToLive);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove all key exists below cache
        /// </summary>
        /// <returns></returns>
        public long CacheRemoveAll()
        {
            var keys = CacheGetAllKey();
            int i = 1;
            if (keys.Any())
            {
                foreach (var item in keys)
                {
                    try
                    {
                        documentClient.DeleteDocumentAsync(GetDocumentUri(item), RequestOptions()).ConfigureAwait(false).GetAwaiter().GetResult();
                        i++;
                    }
                    catch (Exception ex) { throw new Exception(ex.Message); }
                    finally { }
                }
                return (i - 1);
            }
            return 0;
        }

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long CacheRemoveContainsKey(string[] keys)
        {
            if (!_configuration.EnableCache || !keys.Any()) return 0;
            int i = 1;
            var cosmosKeys = new List<string>();
            foreach (var item in keys)
            {
                try
                {
                    var doc = documentClient.CreateDocumentQuery<Document>(UriFactory.CreateDocumentCollectionUri(_configuration.CosmosDataBaseName, _configuration.CosmosCollection)
                            , new FeedOptions { EnableCrossPartitionQuery = true }).Where(p => p.Id.Contains(item)).AsEnumerable();
                    cosmosKeys.AddRange(doc.Select(p => p.Id));
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { }
            }
            if (cosmosKeys.Any())
            {
                foreach (var key in cosmosKeys)
                {
                    try
                    {
                        documentClient.DeleteDocumentAsync(GetDocumentUri(key), RequestOptions()).ConfigureAwait(false).GetAwaiter().GetResult();
                        i++;
                    }
                    catch { }
                    finally { }
                }
                return (i - 1);
            }
            else
                return 0;
        }

        public object CreateDocument(string key, object value, int timeExpired = -1)
        {
            return new CacheResult(key, JsonConvert.SerializeObject(value), timeExpired);
        }

        protected virtual async Task<T> GetDocument<T>(string key)
        {
            var documentResponse = await documentClient.ReadDocumentAsync<CacheResult>(GetDocumentUri(key), RequestOptions()).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(documentResponse.Document.Value);
        }

        public RequestOptions RequestOptions() => new RequestOptions() { PartitionKey = new PartitionKey(Undefined.Value) };

        public string GetDocumentUri(string key) => (UriFactory.CreateDocumentUri(_configuration.CosmosDataBaseName, _configuration.CosmosCollection, key)).ToString();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                documentClient.Dispose();
            }

            disposed = true;
        }
    }
}
