using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caching.Cosmos;

namespace Caching.Core
{
    public interface ICacheCosmos
    {
        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// <para> Default value TimeExpired Cache is equals "-1" (Never Expired) </para>
        /// <para>
        /// Returns: 
        ///         True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CacheAdd<T>(string key, T value, int timeExpired = -1);

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// <para> Default value TimeExpired Cache is equals "-1" (Never Expired) </para>
        /// <para> KeyValuePair<string, T>[]: string is key, T is value below cache </para>
        /// <para>
        /// Returns: 
        ///         True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CacheAdd<T>(KeyValuePair<string, T>[] values);

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// <para> Default value TimeExpired Cache is equals "-1" (Never Expired) </para>
        /// <para> KeyValuePair<string, ValueTuple<T, int>>:
        /// string is key blow cache
        /// </para><para>
        /// ValueTuple<T, int>: T is value and int is TimeExpired Cache </para><para>
        /// Returns:
        ///         True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CacheAdd<T>(KeyValuePair<string, ValueTuple<T, int>>[] values);

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// <para> Default value TimeExpired Cache is equals "-1" (Never Expired) </para>
        /// <para>
        /// Returns:
        ///         True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CacheSet<T>(string key, T value, int timeExpired = -1);

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// <para> Default value TimeExpired Cache is equals "-1" (Never Expired) </para>
        /// KeyValuePair<string, T>[]: string is key blow cache, T is value below cache
        /// <para>
        /// Returns:
        ///         True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CacheSet<T>(KeyValuePair<string, T>[] values);

        /// <summary>
        /// Sets the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// <para>
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// </para>
        /// <para>
        /// KeyValuePair<string, ValueTuple<T, int>>: string is key blow cache 
        /// ValueTuple<T, int>: T is value and int is TimeExpired Cache
        /// </para>
        /// <para>
        /// </para>
        /// Returns:
        ///         True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CacheSet<T>(KeyValuePair<string, ValueTuple<T, int>>[] values);

        /// <summary>
        /// Get the value of key. If the key does not exist the special value null is returned.
        /// <para>
        /// Returns:
        ///         The value of key, or null when key does not exist.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T CacheGet<T>(string key);

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
        bool CacheRemove(string key);

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// <para>
        /// Input is Array keys
        /// </para>
        /// <para>
        /// Returns:
        ///         The number of keys that were removed.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        long CacheRemove(string[] keys);

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist
        /// <para>
        /// Query from cache with contains key later delete all cache from the above query
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        long CacheRemoveContainsKey(string key);

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        long CacheRemoveContainsKey(string[] keys);

        /// <summary>
        /// Returns if key exists.
        /// <para>
        /// Returns:
        ///         1 if the key exists. 0 if the key does not exist.
        /// </para>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool CacheKeyExists(string key);

        /// <summary>
        /// Update time expired cache
        /// <para>
        /// If present and the timeExpired is set to "-1", it is equal to infinity, and items don’t expire by default.
        /// </para>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeExpired"></param>
        /// <returns></returns>
        bool CacheUpdateInterval(string key, int timeExpired = -1);

        /// <summary>
        /// Update time expired cache
        /// <para>
        /// If present and the timeExpired is set to "-1", it is equal to infinity, and items don’t expire by default.
        /// </para>
        /// <para>
        /// KeyValuePair<string, int>: key is blow cache and int is TimeExpired Cache
        /// </para>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeExpired"></param>
        /// <returns></returns>
        bool CacheUpdateInterval<T>(KeyValuePair<string, int>[] values);

        /// <summary>
        /// Update the list key time expired cache
        /// <para>
        /// If present and the timeExpired is set to "-1", it is equal to infinity, and items don’t expire by default.
        /// </para>
        /// </summary>
        /// <param name="containsKey"></param>
        /// <param name="timeExpired"></param>
        /// <returns></returns>
        bool CacheUpdateListKeyInterval(string containsKey, int timeExpired = -1);

        /// <summary>
        /// Update value the given keys to their respective values. If "not exists" is specified, this will not perform any operation at all even if just a single key already
        /// <para>
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// </para>
        /// <para>
        /// Returns: 
        ///         True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CacheUpdateValue<T>(string key, T value, int timeExpired = -1);

        /// <summary>
        /// Update value the given keys to their respective values.
        /// <para>
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// </para>
        /// <para>
        /// KeyValuePair<string, T>[]: string is key blow cache, T is value below cache
        /// </para>
        /// <para>
        /// Returns:
        ///         True if the keys were set, else False
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CacheUpdateValue<T>(KeyValuePair<string, T>[] values);

        /// <summary>
        /// Update value the given keys to their respective values.
        /// <para>
        /// Default value TimeExpired Cache is equals "-1" (Never Expired)
        /// </para>
        /// <para>
        /// KeyValuePair<string, ValueTuple<T, int>>: string is key blow cache, T is value below cache, int is TimeExpired Cache
        /// </para>
        /// Returns:
        ///         True if the keys were set, else False
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CacheUpdateValue<T>(KeyValuePair<string, ValueTuple<T, int>>[] values);

        /// <summary>
        /// Get all key below cache
        /// </summary>
        /// <param name="containskey"></param>
        /// <param name="draft"></param>
        /// <returns></returns>
        List<string> CacheGetAllKey();

        /// <summary>
        /// Update value for cache of the List Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        bool CacheUpdateListValue(List<CacheResult> results);

        /// <summary>
        /// Remove all key exists below cache
        /// </summary>
        /// <returns></returns>
        long CacheRemoveAll();
    }
}
