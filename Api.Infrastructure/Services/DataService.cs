using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Api.Domain.Interfaces;
using Api.Infrastructure.Extensions;
using Api.Infrastructure.Properties;
using Api.Infrastructure.Models;
using System.Linq;
using MongoDB.Bson;

namespace Api.Infrastructure.Services
{
    /// <summary>
    /// Provides base data access functionality. Currently using Mongo.
    /// </summary>
    public class DataService<T> : IDataService<T> where T : DataObject, new()
    {
        private readonly ILogger _logger;
        private readonly IMongoCollection<T> _data;

        /// <summary>
        /// Constructs a data service 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        /// <param name="logger"></param>
        public DataService(IOptionsMonitor<ApiOptions> optionsAccessor, ILogger<DataService<T>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            var options = optionsAccessor?.CurrentValue ?? throw new ArgumentNullException(nameof(optionsAccessor));
           _data = GetOrCreateCollection(options);
        }

        private IMongoCollection<T> GetOrCreateCollection(ApiOptions options)
        {
            _logger.LogDebug("Creating a MongoClient");
            var connectionString = options.MongoDbConnection;
            _logger.LogDebug("Connection string is {0}", connectionString);
            if (connectionString.IsNullOrWhiteSpace())
            {
                throw new ArgumentException(Resources.ConnStringRequired);
            }
            _logger.LogDebug("Getting database {0}", options.DbName);
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(options.DbName);
            var collectionName = typeof(T).Name;
            _logger.LogDebug("Looking for the {0} collection", collectionName);
            var collectionExists = database
                .ListCollectionNames(new ListCollectionNamesOptions { Filter = new BsonDocument("name", collectionName) })
                .Any();
            if (!collectionExists)
            {
                _logger.LogDebug("Collection didn't exist, creating");
                database.CreateCollection(collectionName);
            }
            return database.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Gets a list of data objects
        /// </summary>
        /// <returns>List of data</returns>
        public async Task<IEnumerable<T>> ListAsync()
        {
            _logger.LogDebug("DataService.List");
            return (await _data.FindAsync(t => true)).ToList();
        }

        /// <summary>
        /// Gets a data object by <paramref name="id" />
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetDataAsync(string id)
        {
            _logger.LogDebug("DataService.Get, id: {0}", id);
            var dataObject = (await _data.FindAsync(d => d.Id == id)).FirstOrDefault();

            return null == dataObject ? DataObject.NotCreated as T : dataObject;
        }

        /// <summary>
        /// Creates a data object asynchronously.
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns>Newly created data object or null if it already exists.</returns>
        /// <exception type="ArgumentNullException">Thrown when the <paramref name="dataObject" /> is null.</exception>
        public async Task<T> CreateAsync(T dataObject)
        {
            _logger.LogDebug("DataService.CreateAsync");
            dataObject.ThrowIfNull(nameof(dataObject));
            dataObject.CreatedOn = dataObject.ModifiedOn = DateTime.Now;
            await _data.InsertOneAsync(dataObject);
            return dataObject;
        }

        /// <summary>
        /// Updates a data object by replacing it.
        /// </summary>
        /// <param name="id">T identifier.</param>
        /// <param name="dataIn">Data object to update.</param>
        /// <returns>1 if successful 0 otherwise.</returns>
        public async Task<long> Update(string id, T dataIn)
        {
            _logger.LogDebug("DataService.ReplaceOneAsync, id: {0}", id);
            var result = await _data.ReplaceOneAsync(d => d.Id == id, dataIn);
            return result.ModifiedCount;
        }

        /// <summary>
        /// Removes a data object.
        /// </summary>
        /// <param name="dataIn">Data object to remove.</param>
        /// <returns>1 if successful 0 otherwise.</returns>
        public async Task<long> Remove(T dataIn)
        {
            _logger.LogDebug("DataService.Remove");
            return null == dataIn ? 0 : await Remove(dataIn.Id);
        }

        /// <summary>
        /// Removes a data object by <paramref name="id" />.
        /// </summary>
        /// <param name="id">Data object's identifier.</param>
        /// <returns>1 if successful 0 otherwise.</returns>
        public async Task<long> Remove(string id)
        {
            _logger.LogDebug("DataService.Remove, id: {0}", id);
            var result = await _data.DeleteOneAsync(d => d.Id == id);
            return result.DeletedCount;
        }
    }
}