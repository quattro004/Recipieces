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
using RecipeUIClassLib.Extensions;

namespace RecipeUIClassLib.Areas.Recipes.Pages
{
    [Authorize]
    public class CreateModel : RecipeModel
    {
        public CreateModel(IRecipeService recipeService, ICategoryService categoryService, ILogger<RecipeService> logger) 
            : base(recipeService, categoryService, logger)
        {
            BuildCategories().GetAwaiter().GetResult();
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogDebug("Creating a recipe");
            BuildRecipe();
            Recipe.Category = _categories.SingleOrDefault(c => c.Id == SelectedCategory);
            await _recipeService.CreateAsync(Recipe);

            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}