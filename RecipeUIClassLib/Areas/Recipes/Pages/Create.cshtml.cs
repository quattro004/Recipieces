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
    public class CreateModel : PageModel
    {
        private readonly IRecipeService _recipeService;

        [BindProperty]
        public RecipeViewModel Recipe { get; set; }

        public CreateModel(IRecipeService recipeService)
        {
            _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _recipeService.CreateAsync(Recipe);

            return RedirectToPage("./Index");
        }
    }
}