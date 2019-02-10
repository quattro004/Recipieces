using System.Collections.Generic;
using System.Linq;
using RecipeApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace RecipeApi.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _categories;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IConfiguration config, ILogger<CategoryService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            try
            {
                _logger.LogDebug("Creating a MongoClient");
                var client = new MongoClient(config.GetConnectionString("RecipeDb"));
                _logger.LogDebug("Getting database RecipeDb");
                var database = client.GetDatabase("RecipeDb");
                _logger.LogDebug("Getting the categories collection");
                _categories = database.GetCollection<Category>("Categories");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message, exc);
                throw exc;
            }
        }

        public IEnumerable<Category> Get()
        {
            return _categories.Find(category => true).ToList();
        }

        public Category Get(string id)
        {
            return _categories.Find<Category>(category => category.Id == id).FirstOrDefault();
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _categories.InsertOneAsync(category);
            return category;
        }

        public void Update(string id, Category categoryIn)
        {
            _categories.ReplaceOne(category => category.Id == id, categoryIn);
        }

        public void Remove(Category categoryIn)
        {
            _categories.DeleteOne(category => category.Id == categoryIn.Id);
        }

        public void Remove(string id)
        {
            _categories.DeleteOne(category => category.Id == id);
        }
    }
}