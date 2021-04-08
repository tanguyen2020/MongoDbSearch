using CacheOrSearchEngine.ElasticSearch;
using CacheOrSearchEngine.MongoDB;
using CacheOrSearchEngine.MongoDB.CacheKeyValue;
using CacheOrSearchEngine.MongoDB.Interfaces;
using CacheOrSearchEngine.MongoDB.SearchLikeCharacters;
using CacheOrSearchEngine.RedisCache;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CacheOrSearchEngine.DI
{
    public static class DependencyCacheOrSearch
    {
        public static IServiceCollection AddCacheOrSearch(this IServiceCollection services)
        {
            services.AddSingleton<IRedisCache, CacheOrSearchEngine.RedisCache.RedisCache>();
            services.AddSingleton<IElasticSearchFactory, ElasticSearchFactory>();
            services.AddSingleton<IElasticQuery, ElasticQuery>();
            services.AddSingleton<ICacheMongo, CacheMongo>();
            services.AddSingleton<ISearchMongo, SearchMongo>();
            services.AddSingleton<IMongoDBSearchFactory, MongoDBSearchFactory>();
            services.AddSingleton<IConfigMongDB, ConfigMongDB>();
            return services;
        }
    }
}
