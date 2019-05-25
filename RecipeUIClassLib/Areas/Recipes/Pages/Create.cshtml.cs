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
    public class CreateModel : RecipeModel
    {
        public CreateModel(IRecipeService recipeService, ICategoryService categoryService, ILogger<RecipeService> logger) 
            : base(recipeService, categoryService, logger)
        {
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await BuildCategories();
            
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogDebug("Posting a recipe to the API");
            _logger.LogDebug("Cat is {0}", SelectedCategory);
            
            await BuildCategories();
            
            if (null == SelectedCategory)
            {
                ModelState.AddModelError("SelectedCategory", "Please select a category");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Recipe.Category = _categories.SingleOrDefault(c => c.Id == SelectedCategory);
            BuildLists();
            await _recipeService.CreateAsync(Recipe);

            return RedirectToPage("Index");
        }
    }
}