using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using RecipeApi.Extensions;
using RecipeApi.Models;

namespace RecipeApi.Services
{
    /// <summary>
    /// Provides base data access functionality. Currently using Mongo.
    /// </summary>
    public class DataService<T> where T : DataObject, new()
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config; 
        private readonly IMongoCollection<T> _data;


        /// <summary>
        /// Constructs a data service 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public DataService(IConfiguration config, ILogger<DataService<T>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            try
            {
                _logger.LogDebug("Creating a MongoClient");
                var connectionString = _config.GetConnectionString("RecipeDb");
                
                _logger.LogDebug("Connection string is {0}", connectionString);
                var client = new MongoClient(connectionString);
                
                _logger.LogDebug("Getting database RecipeDb");
                var database = client.GetDatabase("RecipeDb");

                var name = nameof(T);
                _logger.LogDebug("Getting the {0} collection", name);
                _data = database.GetCollection<T>(name);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message, exc);
                throw exc;
            }
        }

        /// <summary>
        /// Gets a list of data objects
        /// </summary>
        /// <returns>List of data</returns>
        public IEnumerable<T> Get()
        {
            return _data.Find(t => true).ToList();
        }

        /// <summary>
        /// Gets a data object by <paramref name="id" />
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(string id)
        {
            var dataObject = _data.Find<T>(d => d.Id == id).FirstOrDefault();

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
            return null == dataIn ? 0 : await Remove(dataIn.Id);
        }

        /// <summary>
        /// Removes a data object by <paramref name="id" />.
        /// </summary>
        /// <param name="id">Data object's identifier.</param>
        /// <returns>1 if successful 0 otherwise.</returns>
        public async Task<long> Remove(string id)
        {
            var result = await _data.DeleteOneAsync(d => d.Id == id);
            return result.DeletedCount;
        }
    }
}