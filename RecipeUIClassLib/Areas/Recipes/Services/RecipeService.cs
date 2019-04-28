using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RecipeUIClassLib.Areas.Recipes.Models;

namespace RecipeUIClassLib.Areas.Recipes.Services
{
    /// <summary>
    /// Manages data from the recipe API.
    /// </summary>
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly RecipeApiOptions _options;
        private readonly ILogger _logger;

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
        }

        /// <summary>
        /// Creates the specified <paramref name="recipe" />
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public async Task CreateAsync(RecipeViewModel recipe)
        {
            _logger.LogDebug("Creating a recipe");
            throw new NotImplementedException();
        }

        /// <summary>
        /// /// Gets a list of all the recipes.
        /// </summary>
        /// <returns>List of <see cref="RecipeViewModel" />.</returns>
        public async Task<IEnumerable<RecipeViewModel>> GetRecipesAsync()
        {
            _logger.LogDebug("Getting all recipes");
            var uri = Path.Combine(_options.RecipeApiBaseUrl, "recipes");
            _logger.LogDebug("RecipeApi base url is {0}", uri);

            var responseString = await _httpClient.GetStringAsync(uri);
            _logger.LogDebug("Got recipes from the API, woot");

            var recipes = JsonConvert.DeserializeObject<IEnumerable<RecipeViewModel>>(responseString);
            return recipes;
        }
    }
}