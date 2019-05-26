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
            SelectedCategory = Recipe.Category.Id;
            Instructions =  Recipe.Instructions.ToDelimited();
            Ingredients = Recipe.Ingredients.ToDelimited();
            Keywords = Recipe.Keywords.ToDelimited();
            Preparation = Recipe.Preparation.ToDelimited();
            
            return Page();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            var page = await base.OnPostAsync();

            if (!ModelState.IsValid)
            {
                return page;
            }

            Recipe.Category = _categories.SingleOrDefault(c => c.Id == SelectedCategory);
            await _recipeService.UpdateAsync(Recipe);

            return RedirectToPage("Index");
        }
    }
}