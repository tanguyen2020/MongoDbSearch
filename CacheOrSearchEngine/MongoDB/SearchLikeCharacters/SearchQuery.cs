using System;
using System.Collections.Generic;
using System.Text;

namespace CacheOrSearchEngine.MongoDB.SearchLikeCharacters
{
    public class SearchQuery: SearchQueryMongoBase
    {
        public SearchQuery(IMongoDBSearchFactory mongoDBSearchFactory, IConfigMongDB configMongDB, string collectionSearch)
            : base(mongoDBSearchFactory, configMongDB, collectionSearch)
        {

        }
    }
}
