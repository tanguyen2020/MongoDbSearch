using System;
using System.Collections.Generic;

/// <summary>
/// Configuration settings cache
/// </summary>
namespace Caching.Core
{
    public interface ICacheConfiguration
    {
        /// <summary>
        /// Atrribute confirm is use Caching
        /// If EnableCache = false is turn off cache
        /// If EnableCache = true is turn on cache
        /// </summary>
        bool EnableCache { get; }

        /// <summary>
        /// Endpoint Uri Azure CosmosDB
        /// </summary>
        string CosmosAccountEndpoint { get; }

        /// <summary>
        /// Primary key Azure CosmosDB
        /// </summary>
        string CosmosAccountKey { get; }

        /// <summary>
        /// DatabaseName on Azure CosmosDB
        /// </summary>
        string CosmosDataBaseName { get; }

        /// <summary>
        /// Container on Azure CosmosDB
        /// </summary>
        string CosmosCollection { get; }

        List<DateTime> CMSReleasedDate { get;}
        DateTime? FindPriodDate(DateTime dateTime);

        /// <summary>
        /// The system will automatically delete items based on the TTL value (in seconds) you provide, without needing a delete operation explicitly issued by a client application
        /// </summary>
        int? CacheTimeExpired { get; }
    }
}
