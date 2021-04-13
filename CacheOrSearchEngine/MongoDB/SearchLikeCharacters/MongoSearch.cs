using CacheOrSearchEngine.MongoDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CacheOrSearchEngine.MongoDB.SearchLikeCharacters
{
    public class MongoSearch : IMongoSearch
    {
        IMongoDBSearchFactory _mongoDBSearchFactory;
        IConfigMongDB _configMongDB;
        public MongoSearch(IMongoDBSearchFactory mongoDBSearchFactory, IConfigMongDB configMongDB)
        {
            _mongoDBSearchFactory = mongoDBSearchFactory;
            _configMongDB = configMongDB;
        }

        public ISearchQueryMongo GetCollection(string collection)
        {
            return new SearchQuery(_mongoDBSearchFactory, _configMongDB, collection);
        }
    }
}
