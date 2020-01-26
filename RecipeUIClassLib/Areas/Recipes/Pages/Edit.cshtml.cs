using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeUIClassLib.Areas.Recipes.Models;
using RecipeUIClassLib.Extensions;
using RecipeUIClassLib.Infrastructure.Interfaces;

namespace RecipeUIClassLib.Areas.Recipes.Pages
{
    [Authorize]
    public class EditModel : RecipeModel
    {
        private readonly IRecipeWebApi _recipeClient;
        private readonly ILogger _logger;

        public EditModel(IRecipeWebApi recipeClient, ICategoryWebApi categoryClient, ILogger<EditModel> logger) 
            : base(categoryClient)
        {
            _recipeClient = recipeClient ?? throw new ArgumentNullException(nameof(recipeClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _logger.LogDebug("Getting a recipe for edit with id {0}", id);
            Recipe = await _recipeClient.GetAsync(id);
            _logger.LogDebug("Got the recipe, id is {0}", id);
            SelectedCategory = Recipe.Category.Id;
            Instructions =  Recipe.Instructions.ToDelimited();
            Ingredients = Recipe.Ingredients.ToDelimited();
            Keywords = Recipe.Keywords.ToDelimited();
            Preparation = Recipe.Preparation.ToDelimited();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogDebug("EditModel.OnPostAsync()");
            _logger.LogDebug("Cat is {0}", SelectedCategory);
            
            BuildRecipe();
            Recipe.Category = Categories.SingleOrDefault(c => c.Id == SelectedCategory);
            ValidateRecipe();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _logger.LogDebug("Updating a recipe with id {0}", Recipe.Id);
                await _recipeClient.UpdateAsync(Recipe.Id, Recipe);
            }
            // TODO: what exceptions should this catch?
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                ModelState.AddModelError("Edit-Recipe", exc.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}