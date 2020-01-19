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
    public class DeleteModel : RecipeModel
    {
        public DeleteModel(IRecipeService recipeService, ICategoryService categoryService, ILogger<DeleteModel> logger) 
            : base(recipeService, categoryService, logger)
        {
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _logger.LogDebug("Getting recipe details with id {0}", id);
            Recipe = await _recipeService.GetAsync(id);
            
            if (Recipe == null)
            {
                return NotFound();
            }

            await BuildCategories();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogDebug("DeleteModel.OnPostAsync()");
            try
            {
                _logger.LogDebug("Deleting a recipe with id {0}", Recipe.Id);
                await _recipeService.DeleteAsync(Recipe.Id);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                ModelState.AddModelError("Delete", exc.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}