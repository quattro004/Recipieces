using System;
using Microsoft.Extensions.Configuration;

namespace Api.Infrastructure
{
    /// <summary>
    /// Contains the API options configuration.
    /// </summary>
    public class ApiOptions
    {
        /// <summary>
        /// Mongo Db connection string.
        /// </summary>
        public string MongoDbConnection { get; set; }
        
        /// <summary>
        /// Mongo database name.
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// Path where the album contents are stored.
        /// </summary>
        public string AlbumContentPath { get; set; }

        /// <summary>
        /// Limit of the size of album content files in bytes.
        /// </summary>
        public long FileSizeLimit { get; set; }
    }
}