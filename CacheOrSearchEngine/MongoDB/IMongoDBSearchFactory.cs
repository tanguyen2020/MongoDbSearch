using MongoDB.Driver;
using System;

namespace CacheOrSearchEngine.MongoDB
{
    public interface IMongoDBSearchFactory
    {
        MongoClient MongoClient(string connectionString);
    }
}
