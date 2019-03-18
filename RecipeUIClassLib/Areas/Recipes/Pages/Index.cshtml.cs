using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeUIClassLib.Areas.Recipes.Models;
using RecipeUIClassLib.Areas.Recipes.Services;

namespace RecipeUIClassLib.Areas.Recipes.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRecipeService _recipeService;

        public IEnumerable<RecipeViewModel> Recipes { get; set; }

        public IndexModel(IRecipeService recipeService)
        {
            _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Recipes = await _recipeService.GetRecipes();

            return Page();
        }
    }
}