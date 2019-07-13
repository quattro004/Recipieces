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
        public EditModel(IRecipeService recipeService, ICategoryService categoryService, ILogger<EditModel> logger) 
            : base(recipeService, categoryService, logger)
        {
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _logger.LogDebug("Getting a recipe for edit with id {0}", id);
            Recipe = await _recipeService.GetAsync(id);
            _logger.LogDebug("Got the recipe, id is {0}", id);
            await BuildCategories();
            SelectedCategory = Recipe.Category.Id;
            Instructions =  Recipe.Instructions.ToDelimited();
            Ingredients = Recipe.Ingredients.ToDelimited();
            Keywords = Recipe.Keywords.ToDelimited();
            Preparation = Recipe.Preparation.ToDelimited();
            PrepTime = $"{Recipe.PrepTime.Hours}:{Recipe.PrepTime.Minutes}";
            CookTime = $"{Recipe.CookTime.Hours}:{Recipe.CookTime.Minutes}";
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogDebug("EditModel.OnPostAsync()");
            _logger.LogDebug("Cat is {0}", SelectedCategory);
            
            BuildRecipe();
            await BuildCategories();
            Recipe.Category = _categories.SingleOrDefault(c => c.Id == SelectedCategory);
            ValidateRecipe();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _logger.LogDebug("Updating a recipe with id {0}", Recipe.Id);
                await _recipeService.UpdateAsync(Recipe);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                ModelState.AddModelError("Edit", exc.Message);
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}