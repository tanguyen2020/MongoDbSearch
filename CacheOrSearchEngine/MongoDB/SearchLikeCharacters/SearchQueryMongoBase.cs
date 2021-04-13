using CacheOrSearchEngine.MongoDB.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseObject.DataObject;

namespace CacheOrSearchEngine.MongoDB.SearchLikeCharacters
{
    public abstract class SearchQueryMongoBase : ISearchQueryMongo
    {
        private readonly IConfigMongDB _configMongDB;
        private readonly IMongoDBSearchFactory _mongoDBSearchFactory;
        private readonly string _collectionSearch;
        private readonly IMongoDatabase _database;
        public SearchQueryMongoBase(IMongoDBSearchFactory mongoDBSearchFactory, IConfigMongDB configMongDB, string collectionSearch)
        {
            _mongoDBSearchFactory = mongoDBSearchFactory;
            _configMongDB = configMongDB;
            _database = _mongoDBSearchFactory.MongoClient(_configMongDB.ConnectionString).GetDatabase(_configMongDB.DatabaseMongDB);
            _collectionSearch = collectionSearch;
        }


        public IMongoCollection<DataObject> Collection => _database.GetCollection<DataObject>(_collectionSearch);

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
            Collection.InsertMany(documents);
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
            var all = Collection.Find(_ => true).ToList();
            var values = all.Select(o => o["Value"]);
            var deletes = await Collection.DeleteManyAsync(Builders<DataObject>.Filter.In(o => o["Value"], values));
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
            await Collection.InsertManyAsync(documents);
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
                return await Collection.Find($"{{value: /{item}/ }}").ToListAsync();
            }
            catch
            {
                return null;
            }
            finally { }
        }
    }
}
