using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RecipeUIClassLib.Areas.Recipes.Models;

namespace RecipeUIClassLib.Areas.Recipes.Services
{
    /// <summary>
    /// Manages data from the category API.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        /// <summary>
        /// Constructs a <see cref="CategoryService" /> using a typed <see cref="HttpClient" />.
        /// </summary>
        /// <param name="httpClient"></param>
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            // TODO: send this in from config
            _remoteServiceBaseUrl = "http://localhost:5000/api/";
        }

        /// <summary>
        /// Gets a list of all the categories.
        /// </summary>
        /// <returns>List of <see cref="CategoryViewModel" />.</returns>
        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            var uri = Path.Combine(_remoteServiceBaseUrl, "categories");

            var responseString = await _httpClient.GetStringAsync(uri);

            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(responseString);
            return categories;
        }
    }
}