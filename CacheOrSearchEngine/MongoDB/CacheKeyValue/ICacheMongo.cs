using CacheOrSearchEngine.MongoDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CacheOrSearchEngine.MongoDB.CacheKeyValue
{
    public interface ICacheMongo: ICacheMongoAsync
    {
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
        T Get<T>(string key);

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
        bool Remove(string key);

        /// <summary>
        /// Delete all the keys of all databases on the server.
        /// </summary>
        bool RemoveAll();

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
        bool Add<T>(string key, T item);
    }
}
