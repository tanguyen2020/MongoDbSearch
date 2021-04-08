using CacheOrSearchEngine.MongoDB.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObject.DataObject;

namespace CacheOrSearchEngine.MongoDB.SearchLikeCharacters
{
    public class SearchMongo : ISearchMongo
    {
        private readonly IConfigMongDB _configMongDB;
        private readonly IMongoDBSearchFactory _mongoDBSearchFactory;
        private readonly IMongoCollection<DataObject> _collection;
        public SearchMongo(IMongoDBSearchFactory mongoDBSearchFactory, IConfigMongDB configMongDB)
        {
            _mongoDBSearchFactory = mongoDBSearchFactory;
            _configMongDB = configMongDB;
            _collection = _mongoDBSearchFactory.MongoClient(_configMongDB.ConnectionString)
                        .GetDatabase(_configMongDB.DatabaseMongDB).GetCollection<DataObject>(_configMongDB.CollectionSearch);
        }

        /// <summary>
        /// Delete all value on the collection
        /// Returns:
        ///         The result of the delete operation.
        /// </summary>
        /// <returns></returns>
        public bool RemoveAll() => RemoveAllAsync().Result;

        /// <summary>
        /// Add data for collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documents"></param>
        public void Add(IEnumerable<DataObject> documents)
        {
            _collection.InsertMany(documents);
        }

        /// <summary>
        /// Search like characters
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public List<DataObject> Search(string item) => SearchAsync(item).Result;

        /// <summary>
        /// Delete all value on the collection
        /// <para>
        /// Returns:
        ///         The result of the delete operation.
        /// </para>
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RemoveAllAsync()
        {
            var all = _collection.Find(_ => true).ToList();
            var values = all.Select(o => o["Value"]);
            var deletes = await _collection.DeleteManyAsync(Builders<DataObject>.Filter.In(o => o["Value"], values));
            if (deletes.DeletedCount > 0) return true;
            return false;
        }

        /// <summary>
        /// Insert many documents
        /// <para>
        /// Returns:
        ///         The result of the insert operation.
        /// </para>
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public async Task AddAsync(IEnumerable<DataObject> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        /// <summary>
        /// Search like characters
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<List<DataObject>> SearchAsync(string item)
        {
            try
            {
                return await _collection.Find($"{{value: /{item}/ }}").ToListAsync();
            }
            catch
            {
                return null;
            }
            finally { }
        }
    }
}
