using System;

namespace CacheOrSearchEngine.MongoDB
{
    public class ConfigMongDB : IConfigMongDB
    {
        public string DatabaseMongDB => throw new NotImplementedException();
        public string CollectionCacheKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CollectionSearch { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ConnectionString => throw new NotImplementedException();
    }
}
