using MongoDB.Driver;

namespace CacheOrSearchEngine.MongoDB
{
    public interface IMongoDBSearchFactory
    {
        MongoClient MongoClient(string connectionString);
    }
}
