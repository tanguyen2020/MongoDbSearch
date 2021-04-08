using BaseObject.DataObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CacheOrSearchEngine.MongoDB.Interfaces
{
    public interface ISearchMongoAsync
    {
        /// <summary>
        /// Search like characters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<List<DataObject>> SearchAsync(string item);

        /// <summary>
        /// Delete all value on the collection
        /// <para>
        /// Returns:
        ///         The result of the delete operation.
        /// </para>
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveAllAsync();

        /// <summary>
        /// Add data for collection
        /// <para>
        /// Returns:
        ///         The result of the insert operation.
        /// </para>
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        Task AddAsync(IEnumerable<DataObject> documents);
    }
}
