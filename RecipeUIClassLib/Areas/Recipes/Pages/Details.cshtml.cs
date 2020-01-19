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
        public DetailsModel(IRecipeService recipeService, ICategoryService categoryService, ILogger<DetailsModel> logger) 
            : base(recipeService, categoryService, logger)
        {
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _logger.LogDebug("Getting recipe details with id {0}", id);
            Recipe = await _recipeService.GetAsync(id);
            
            return Page();
        }
    }
}