using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using RecipeUIClassLib.Areas.Recipes.Models;
using RecipeUIClassLib.Extensions;

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
                _logger.LogDebug("Posting a recipe to the API");
                var response = await _httpClient.PostAsync(_recipeUri,
                    new StringContent(JsonSerializer.Serialize(recipe), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
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
        public async Task<IEnumerable<RecipeViewModel>> ListAsync()
        {
            _logger.LogDebug("Getting all recipes");
            var responseString = await _httpClient.GetStringAsync(_recipeUri);
            if (responseString.IsNullOrWhiteSpace())
            {
                return null;
            }
            _logger.LogDebug("Got recipes from the API, woot");

            var recipes = JsonSerializer.Deserialize<IEnumerable<RecipeViewModel>>(responseString);
            return recipes;
        }

        /// <summary>
        /// Updates the specified <paramref name="recipe" />
        /// </summary>
        /// <param name="recipe">Recipe data being updated, all properties are replaced.</param>
        public async Task UpdateAsync(RecipeViewModel recipe)
        {
            try
            {
                if (null == recipe)
                {
                    throw new ArgumentNullException(nameof(recipe));
                }
                var recipeData = JsonSerializer.Serialize(recipe);
                _logger.LogDebug(recipeData);
                var response = await _httpClient.PutAsync($"{_recipeUri}/{recipe.Id}",
                    new StringContent(recipeData, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
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
        public async Task<RecipeViewModel> GetAsync(string id)
        {
            _logger.LogDebug("Getting recipe from the API with id {0}", id);
            var responseString = await _httpClient.GetStringAsync($"{_recipeUri}/{id}");
            _logger.LogDebug(responseString);
            if (responseString.IsNullOrWhiteSpace())
            {
                return null;
            }
            var recipe = JsonSerializer.Deserialize<RecipeViewModel>(responseString);
            return recipe;
        }

        /// <summary>
        /// Deletes a recipe by <paramref name="id" />.
        /// </summary>
        /// <param name="id">Recipe identifier.</param>        
        public async Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogDebug("Deleting recipe from the API with id {0}", id);
                var response = await _httpClient.DeleteAsync($"{_recipeUri}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Failed to delete the recipe");
                throw exc;
            }
        }
    }
}