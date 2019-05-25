using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecipeUIClassLib.Areas.Recipes.Models;
using RecipeUIClassLib.Areas.Recipes.Services;

namespace RecipeUIClassLib.Areas.Recipes.Pages
{
    [Authorize]
    public class EditModel : RecipeModel
    {
        public EditModel(IRecipeService recipeService, ICategoryService categoryService, ILogger<RecipeService> logger) 
            : base(recipeService, categoryService, logger)
        {
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _logger.LogDebug("Getting a recipe with id {0}", id);
            Recipe = await _recipeService.GetRecipeAsync(id);

            await BuildCategories();
            
            return Page();
        }

        public async Task<IActionResult> OnPutAsync()
        {
            _logger.LogDebug("Putting a recipe to the API");
            _logger.LogDebug("Cat is {0}", SelectedCategory);
            
            await BuildCategories();
            BuildLists();
            ValidateRecipe();
                        
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Recipe.Category = _categories.SingleOrDefault(c => c.Id == SelectedCategory);
            await _recipeService.UpdateAsync(Recipe);

            return RedirectToPage("Index");
        }
    }
}