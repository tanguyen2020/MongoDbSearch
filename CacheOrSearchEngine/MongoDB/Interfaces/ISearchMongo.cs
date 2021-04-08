using CacheOrSearchEngine.MongoDB.SearchLikeCharacters;
using System.Collections.Generic;
using BaseObject.DataObject;

namespace CacheOrSearchEngine.MongoDB.Interfaces
{
    public interface ISearchMongo: ISearchMongoAsync
    {
        /// <summary>
        /// Search like characters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        List<DataObject> Search(string item);

        /// <summary>
        /// Delete all value on the collection
        /// Returns:
        ///         The result of the delete operation.
        /// </summary>
        /// <returns></returns>
        bool RemoveAll();

        /// <summary>
        /// Add data for collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documents"></param>
        void Add(IEnumerable<DataObject> documents);
    }
}
