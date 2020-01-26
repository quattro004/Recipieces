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
        private readonly IRecipeWebApi _recipeClient;
        private readonly ILogger _logger;

        public DeleteModel(IRecipeWebApi recipeClient, ICategoryWebApi categoryClient, ILogger<DeleteModel> logger) 
            : base(categoryClient)
        {
            _recipeClient = recipeClient ?? throw new ArgumentNullException(nameof(recipeClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _logger.LogDebug("Getting recipe details with id {0}", id);
            Recipe = await _recipeClient.GetAsync(id);
            
            if (Recipe == null)
            {
                return NotFound();
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogDebug("DeleteModel.OnPostAsync()");
            try
            {
                _logger.LogDebug("Deleting a recipe with id {0}", Recipe.Id);
                await _recipeClient.DeleteAsync(Recipe.Id);
            }
            // TODO: what exceptions should this catch?
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