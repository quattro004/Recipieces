using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using RecipeUIClassLib.Areas.Recipes.Models;

namespace RecipeUIClassLib.Areas.Recipes.Services
{
    /// <summary>
    /// Manages data from the category API.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly RecipeApiOptions _options;

        /// <summary>
        /// Constructs a <see cref="CategoryService" /> using a typed <see cref="HttpClient" />.
        /// </summary>
        /// <param name="httpClient"></param>
        public CategoryService(HttpClient httpClient, IOptionsMonitor<RecipeApiOptions> optionsAccessor, 
            ILogger<RecipeService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = optionsAccessor.CurrentValue ?? throw new ArgumentNullException(nameof(optionsAccessor));
        }

        /// <summary>
        /// Gets a list of all the categories.
        /// </summary>
        /// <returns>List of <see cref="CategoryViewModel" />.</returns>
        public async Task<IEnumerable<CategoryViewModel>> List()
        {
            _logger.LogDebug("Getting all categories");
            var uri = Path.Combine(_options.RecipeApiBaseUrl, "categories");
            _logger.LogDebug("RecipeApi url is {0}", uri);
            var categories = JsonSerializer.Deserialize<IEnumerable<CategoryViewModel>>(await _httpClient.GetStringAsync(uri),
                new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
            _logger.LogDebug("Got categories from the API, woot");

            return categories;
        }
    }
}