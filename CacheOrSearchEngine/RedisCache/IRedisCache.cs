using System;
using System.Collections.Generic;
using System.Text;

namespace CacheOrSearchEngine.RedisCache
{
    public interface IRedisCache: IDisposable
    {
        /// <summary>
        /// Get the value of key. If the key does not exist the special value nil is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

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
        bool Add<T>(string key, T value, TimeSpan? expiry = null);

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
        bool Add<T>(KeyValuePair<string, T>[] keyValues);

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// <para>
        /// Returns: True if the key was removed.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// <para>
        /// Returns: True if the key was removed.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        long Remove(string[] keys);

        /// <summary>
        /// Delete all the keys of all databases on the server.
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// Returns if key exists.
        /// <para>
        /// Returns:
        ///     1 if the key exists. 0 if the key does not exist.
        ///</para>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);
    }
}
