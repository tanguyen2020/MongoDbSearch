using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace CacheOrSearchEngine.MongoDB.CacheKeyValue
{
    public class CacheMongo : ICacheMongo
    {
        private readonly IConfigMongDB _configMongDB;
        private readonly IMongoDBSearchFactory _mongoDBSearchFactory;
        private readonly IMongoCollection<CacheResult> _collection;
        public CacheMongo(IConfigMongDB configMongDB, IMongoDBSearchFactory mongoDBSearchFactory)
        {
            _configMongDB = configMongDB;
            _mongoDBSearchFactory = mongoDBSearchFactory;
            _collection = _mongoDBSearchFactory.MongoClient(_configMongDB.ConnectionString)
                        .GetDatabase(_configMongDB.DatabaseMongDB).GetCollection<CacheResult>(_configMongDB.CollectionCacheKey);
        }

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// <para> 
        /// Returns: 
        ///         True if the key was removed.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key) => RemoveAsync(key).Result;

        /// <summary>
        /// Delete all the keys of all databases on the server.
        /// <para>
        /// Returns:
        ///         True if the key was removed.
        /// </para>
        /// </summary>
        public bool RemoveAll() => RemoveAllAsync().Result;

        /// <summary>
        /// Get the value of key. If the key does not exist the special value null is returned.
        /// <para>
        /// Returns: 
        ///         the value of key, or null when key does not exist.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key) => GetAsync<T>(key).Result;

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// <para> 
        /// Returns: 
        ///         True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add<T>(string key, T item) => AddAsync(key, item).Result;

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// <para> 
        /// Returns: 
        ///         True if the key was removed.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key)
        {
            var result = await _collection.DeleteOneAsync($"{{ key: '{key}' }}");
            if (result.IsAcknowledged) return true;
            else return false;
        }

        /// <summary>
        /// Delete all the keys of all databases on the server.
        /// <para>
        /// Returns:
        ///         The result of the delete operation.
        /// </para>
        /// </summary>
        public async Task<bool> RemoveAllAsync()
        {
            var all = _collection.Find(_ => true).ToList();
            var keys = all.Select(o => o.key);
            var deletes = await _collection.DeleteManyAsync(Builders<CacheResult>.Filter.In(o => o.key, keys));
            if (deletes.DeletedCount > 0) return true;
            return false;
        }

        /// <summary>
        /// Get the value of key. If the key does not exist the special value null is returned.
        /// <para>
        /// Returns: 
        ///         the value of key, or null when key does not exist.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            var result = await _collection.FindAsync($"{{key: {key}}}");
            if (result.Any())
            {
                return JsonConvert.DeserializeObject<T>(result.FirstOrDefault().Value);
            }
            return default(T);
        }

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// <para> 
        /// Returns: 
        ///         True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync<T>(string key, T item)
        {
            try
            {
                string cacheValue = JsonConvert.SerializeObject(item);
                await _collection.InsertOneAsync(new CacheResult(key, JsonConvert.SerializeObject(item)));
                return true;
            }
            catch
            {
                return false;
            }
            finally { }
        }
    }
}
