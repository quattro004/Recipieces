using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RecipeUIClassLib.Areas.Recipes.Models;

namespace RecipeUIClassLib.Areas.Recipes.Services
{
    /// <summary>
    /// Manages data for the recipe API.
    /// </summary>
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly RecipeApiOptions _options;
        private readonly ILogger _logger;
        private readonly string _recipeUri;

        /// <summary>
        /// Constructs a <see cref="RecipeService" /> using a typed <see cref="HttpClient" />.
        /// </summary>
        /// <param name="httpClient"></param>
        public RecipeService(HttpClient httpClient, IOptionsMonitor<RecipeApiOptions> optionsAccessor, 
            ILogger<RecipeService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = optionsAccessor.CurrentValue ?? throw new ArgumentNullException(nameof(optionsAccessor));
            _recipeUri = Path.Combine(_options.RecipeApiBaseUrl, "recipes");            
            _logger.LogDebug("RecipeApi url is {0}", _recipeUri);
        }

        /// <summary>
        /// Creates the specified <paramref name="recipe" />
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public async Task CreateAsync(RecipeViewModel recipe)
        {
            try
            {
                _logger.LogDebug("Creating a recipe");
                var response = await _httpClient.PostAsync(_recipeUri,
                    new StringContent(JsonConvert.SerializeObject(recipe), Encoding.UTF8, "application/json"));   
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Failed to create a recipe");
                throw exc;
            }
        }

        /// <summary>
        /// Gets a list of all the recipes.
        /// </summary>
        /// <returns>List of <see cref="RecipeViewModel" />.</returns>
        public async Task<IEnumerable<RecipeViewModel>> GetRecipesAsync()
        {
            _logger.LogDebug("Getting all recipes");
            var responseString = await _httpClient.GetStringAsync(_recipeUri);
            _logger.LogDebug("Got recipes from the API, woot");

            var recipes = JsonConvert.DeserializeObject<IEnumerable<RecipeViewModel>>(responseString);
            return recipes;
        }

        /// <summary>
        /// Updates the specified <paramref name="recipe" />
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public async Task UpdateAsync(RecipeViewModel recipe)
        {
            try
            {
                if (null == recipe)
                {
                    throw new ArgumentNullException(nameof(recipe));
                }
                _logger.LogDebug("Updating a recipe with id {0}", recipe.Id);
                var response = await _httpClient.PutAsync(_recipeUri,
                    new StringContent(JsonConvert.SerializeObject(recipe), Encoding.UTF8, "application/json"));   
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Failed to update the recipe");
                throw exc;
            }
        }

        /// <summary>
        /// Gets a recipe by <paramref name="id" />.
        /// </summary>
        /// <returns><see cref="RecipeViewModel" /></returns>
        public async Task<RecipeViewModel> GetRecipeAsync(string id)
        {
            _logger.LogDebug("Getting recipe with id {0}", id);
            var responseString = await _httpClient.GetStringAsync($"{_recipeUri}/{id}");

            var recipe = JsonConvert.DeserializeObject<RecipeViewModel>(responseString);
            return recipe;
        }
    }
}