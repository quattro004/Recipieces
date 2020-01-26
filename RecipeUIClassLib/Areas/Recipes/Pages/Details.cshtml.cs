using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeUIClassLib.Areas.Recipes.Models;
using RecipeUIClassLib.Infrastructure.Interfaces;

namespace RecipeUIClassLib.Areas.Recipes.Pages
{
    [Authorize]
    public class DetailsModel : RecipeModel
    {
        private readonly IRecipeWebApi _recipeClient;
        private readonly ILogger _logger;

        public DetailsModel(IRecipeWebApi recipeClient, ICategoryWebApi categoryClient, ILogger<DetailsModel> logger) 
            : base(categoryClient)
        {
            _recipeClient = recipeClient ?? throw new ArgumentNullException(nameof(recipeClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _logger.LogDebug("Getting recipe details with id {0}", id);
            Recipe = await _recipeClient.GetAsync(id);
            
            return Page();
        }
    }
}