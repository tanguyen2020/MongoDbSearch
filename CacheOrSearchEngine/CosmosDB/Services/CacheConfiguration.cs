using System;
using System.Collections.Generic;
using Caching.Core;

namespace Caching.Services
{
    public class CacheConfiguration: ICacheConfiguration
    {
        private DateTime _releasedDate;
        private static List<DateTime> _cmsReleasedDates = new List<DateTime>();
        private static bool _enableCache = false;
        private static int _cacheTimeExpired = 0;
        public CacheConfiguration()
        {
            //Boolean.TryParse(WebConfigurationManager.AppSettings["EnableCache"], out _enableCache);

            //if (WebConfigurationManager.AppSettings["CMSReleasedDate"] != null)
            //{
            //    var realeaseDates = WebConfigurationManager.AppSettings["CMSReleasedDate"].ToString().Split(';');
            //    foreach (var item in realeaseDates)
            //    {
            //        if (DateTime.TryParse(item, out var resultDate))
            //        {
            //            _cmsReleasedDates.Add(resultDate);
            //        }
            //    }
            //}
            //int.TryParse(WebConfigurationManager.AppSettings["CacheTimeExpired"], out _cacheTimeExpired);
        }

        /// <summary>
        /// If EnableCache = false is turn off cache
        /// If EnableCache = true is turn on cache
        /// </summary>
        public bool EnableCache => _enableCache;

        /// <summary>
        /// Endpoint Uri CosmosDB
        /// </summary>
        public string CosmosAccountEndpoint => WebConfigurationManager.AppSettings["CosmosAccountEndpoint"]?.ToString();

        /// <summary>
        /// Primary key for CosmosDB
        /// </summary>
        public string CosmosAccountKey => WebConfigurationManager.AppSettings["CosmosAccountKey"]?.ToString();

        /// <summary>
        /// DatabaseName for CosmosDB
        /// </summary>
        public string CosmosDataBaseName => WebConfigurationManager.AppSettings["CosmosDataBaseName"]?.ToString();

        /// <summary>
        /// Container for CosmosDB
        /// </summary>
        public string CosmosCollection => WebConfigurationManager.AppSettings["CosmosCollection"]?.ToString();

        /// <summary>
        /// Find datetime nearest the create cache with List ReleaseCache settings
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public DateTime? FindPriodDate(DateTime datetime)
        {
            if (datetime == null) return null;
            TimeSpan tmp = TimeSpan.MaxValue;
            _releasedDate = datetime;
            foreach (var item in _cmsReleasedDates)
            {
                if (datetime.CompareTo(item) < 0) continue;
                var timeSubstract = datetime.Subtract(item);
                if (tmp > timeSubstract)
                {
                    tmp = timeSubstract;
                    _releasedDate = item;
                }
            }
            return _releasedDate;
        }
        public List<DateTime> CMSReleasedDate => _cmsReleasedDates;

        /// <summary>
        /// The system will automatically delete items based on the TTL value(in seconds) you provide, without needing a delete operation explicitly issued by a client application
        /// </summary>
        public int? CacheTimeExpired => _cacheTimeExpired == 0 || _cacheTimeExpired < 0 ? -1 : _cacheTimeExpired;
    }
}
