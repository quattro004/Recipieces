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
    /// Manages data from the recipe API.
    /// </summary>
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        /// <summary>
        /// Constructs a <see cref="RecipeService" /> using a typed <see cref="HttpClient" />.
        /// </summary>
        /// <param name="httpClient"></param>
        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            // TODO: send this in from config
            _remoteServiceBaseUrl = "http://localhost:5000/api/";
        }

        /// <summary>
        /// Gets a list of all the recipes.
        /// </summary>
        /// <returns>List of <see cref="RecipeViewModel" />.</returns>
        public async Task<IEnumerable<RecipeViewModel>> GetRecipes()
        {
            var uri = Path.Combine(_remoteServiceBaseUrl, "recipes");

            var responseString = await _httpClient.GetStringAsync(uri);

            var recipes = JsonConvert.DeserializeObject<IEnumerable<RecipeViewModel>>(responseString);
            return recipes;
        }
    }
}