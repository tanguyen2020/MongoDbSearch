using System;
using System.Collections.Generic;
using System.Text;

namespace CacheOrSearchEngine.MongoDB.Interfaces
{
    public interface IMongoSearch
    {
        ISearchQueryMongo GetCollection(string collection);
    }
}
