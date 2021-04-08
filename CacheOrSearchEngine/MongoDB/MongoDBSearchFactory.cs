using MongoDB.Driver;

namespace CacheOrSearchEngine.MongoDB
{
    public class MongoDBSearchFactory: IMongoDBSearchFactory
    {
        public MongoClient MongoClient(string connectionString)
        {
            return new MongoClient(connectionString);
        }
    }
}
