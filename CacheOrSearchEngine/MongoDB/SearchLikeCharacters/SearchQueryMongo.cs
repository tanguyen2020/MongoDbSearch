using CacheOrSearchEngine.MongoDB.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseObject.DataObject;
using MongoDB.Bson;

namespace CacheOrSearchEngine.MongoDB.SearchLikeCharacters
{
    public class SearchQueryMongo : ISearchQueryMongo
    {
        private readonly IConfigMongDB _configMongDB;
        private readonly IMongoDBSearchFactory _mongoDBSearchFactory;
        private readonly string _collectionSearch;
        private readonly IMongoDatabase _database;
        public SearchQueryMongo(IMongoDBSearchFactory mongoDBSearchFactory, IConfigMongDB configMongDB, string collectionSearch)
        {
            _mongoDBSearchFactory = mongoDBSearchFactory;
            _configMongDB = configMongDB;
            _database = _mongoDBSearchFactory.MongoClient(_configMongDB.ConnectionString).GetDatabase(_configMongDB.DatabaseMongDB);
            _collectionSearch = collectionSearch;
        }

        public IMongoCollection<DataObject> Collection => _database.GetCollection<DataObject>(_collectionSearch);


        public async Task<bool> CreateCollectionAsync(string collection)
        {
            try
            {
                await _database.CreateCollectionAsync(collection);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateCollection(string collection) => CreateCollectionAsync(collection).Result;

        public bool Exists() => ExistsAsync().Result;

        public async Task<bool> ExistsAsync()
        {
            var filter = new BsonDocument("name", _collectionSearch);
            var options = new ListCollectionNamesOptions { Filter = filter };
            return await _database.ListCollectionNames(options).AnyAsync();
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
            if (Exists())
            {
                Collection.InsertMany(documents);
            }
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
            if (!Exists()) return false;
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
            if (Exists())
            {
                await Collection.InsertManyAsync(documents);
            }
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
                if (!Exists()) return null;
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
