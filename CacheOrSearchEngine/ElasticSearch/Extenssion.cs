using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheOrSearchEngine.ElasticSearch
{
    public static class Extenssion
    {
        public static BuildBody BuildBodySearch(string queryCondition, IEnumerable<string> memberInfos)
        {
            var multiMatch = new MultiMatch()
            {
                Query = queryCondition,
                TypeSearch = TypeSearchMatch.PhrasePrefix,
                Fields = memberInfos.ToArray()
            };

            var must = new Must() { MultiMatch = multiMatch };

            var boolSearch = new BoolSearch() { Must = new List<Must>() { must} };

            var query = new Query() { Bool = boolSearch };
            var order = new OrderBy() { Order = "asc" };
            var sort = new Sort() { Score = order };

            var body = new BuildBody()
            {
                Sort = new List<Sort>() { sort },
                Query = query
            };
            return body;
        }

        public static BuildBodyDelete BuildBodyDelete(string queryCondition, IEnumerable<string> memberInfos)
        {
            var multiMatch = new MultiMatch()
            {
                Query = queryCondition,
                TypeSearch = TypeSearchMatch.PhrasePrefix,
                Fields = memberInfos.ToArray()
            };

            var must = new Must() { MultiMatch = multiMatch };

            var boolSearch = new BoolSearch() { Must = new List<Must>() { must } };

            var query = new Query() { Bool = boolSearch };

            var body = new BuildBodyDelete()
            {
                Query = query
            };
            return body;
        }
    }
}
