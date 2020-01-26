using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeUIClassLib.Areas.Recipes.Models;
using RecipeUIClassLib.Infrastructure.Interfaces;

namespace RecipeUIClassLib.Areas.Recipes.Pages
{
    [Authorize]
    public class CreateModel : RecipeModel
    {
        private readonly IRecipeWebApi _recipeClient;
        private ILogger _logger;

        public CreateModel(IRecipeWebApi recipeClient, ICategoryWebApi categoryClient, ILogger<CreateModel> logger) 
            : base(categoryClient)
        {
            _recipeClient = recipeClient ?? throw new ArgumentNullException(nameof(recipeClient));
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogDebug("Creating a recipe");
            try
            {
                BuildRecipe();
                Recipe.Category = Categories.SingleOrDefault(c => c.Id == SelectedCategory);
                await _recipeClient.CreateAsync(Recipe);

                if (!ModelState.IsValid)
                {
                    return Page();
                }
            }
            // TODO: what exceptions should this catch?
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                ModelState.AddModelError("Create-Recipe", exc.Message);
                return Page();
            }
            
            return RedirectToPage("./Index");
        }
    }
}