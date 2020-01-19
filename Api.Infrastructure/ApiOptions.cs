using System;

namespace Api.Infrastructure
{
    /// <summary>
    /// Contains the API options configuration.
    /// </summary>
    public class ApiOptions
    {
        /// <summary>
        /// Data connection string.
        /// </summary>
        public string ConnectionString { get; set; }
        
        /// <summary>
        /// Mongo database name.
        /// </summary>
        public string DbName { get; set; }
    }
}