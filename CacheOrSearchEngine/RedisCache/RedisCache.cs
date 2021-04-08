using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Linq;

namespace CacheOrSearchEngine.RedisCache
{
    public class RedisCache : IRedisCache
    {
        private ConfigRedis _configProvider;
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _dbCache;
        public RedisCache(ConfigRedis configProvider)
        {
            _configProvider = configProvider;
            _connection = ConnectionMultiplexer.Connect(_configProvider.ConnectString);
            _dbCache = _connection.GetDatabase(0);
        }

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// <para>
        /// Returns: True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add<T>(string key, T value, TimeSpan? expiry = null) => _dbCache.StringSet(key, JsonConvert.SerializeObject(value), expiry);

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// <para>
        /// Returns: True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add<T>(KeyValuePair<string, T>[] keyValues)
        {
            var redisValue = new List<KeyValuePair<RedisKey, RedisValue>>();
            foreach(var item in keyValues)
            {
                redisValue.Add(new KeyValuePair<RedisKey, RedisValue>(item.Key, JsonConvert.SerializeObject(item.Value)));
            }
            return _dbCache.StringSet(redisValue.ToArray());
        }

        /// <summary>
        /// Get the value of key. If the key does not exist the special value nil is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            var value = _dbCache.StringGet(key);
            if (value.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(value.ToString());
            }
            return default(T);
        }

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// <para>
        /// Returns: True if the key was removed.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key) => _dbCache.KeyDelete(key);

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// <para>
        /// Returns: True if the key was removed.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public long Remove(string[] keys)
        {
            var redisKey = Array.ConvertAll(keys, item => (RedisKey)item);
            return _dbCache.KeyDelete(redisKey);
        }

        /// <summary>
        /// Delete all the keys of all databases on the server.
        /// </summary>
        public void RemoveAll()
        {
            if(_connection.IsConnected)
            {
                var endpoints = _connection.GetEndPoints();
                var server = _connection.GetServer(endpoints.First());
                server.FlushAllDatabases();
            }
        }

        /// <summary>
        /// Returns if key exists.
        /// <para>
        /// Returns:
        ///     1 if the key exists. 0 if the key does not exist.
        ///</para>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key) => _dbCache.KeyExists(key);

        protected bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                _connection.Dispose();
            }
            disposed = true;
        }
    }
}
