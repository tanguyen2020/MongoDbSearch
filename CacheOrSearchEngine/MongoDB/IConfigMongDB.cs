namespace CacheOrSearchEngine.MongoDB
{
    public interface IConfigMongDB
    {
        string ConnectionString { get; }
        string DatabaseMongDB { get; }
        string CollectionCacheKey { get; set; }
        string CollectionSearch { get; set; }
    }
}
