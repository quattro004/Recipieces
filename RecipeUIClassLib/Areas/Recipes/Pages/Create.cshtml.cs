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
    public class CreateModel : PageModel
    {
        private readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger _logger;
        private IEnumerable<CategoryViewModel> _categories;

        [BindProperty]
        public RecipeViewModel Recipe { get; set; }

        public SelectList Categories { get; set; }

        [BindProperty]
        public string SelectedCategory { get; set; }
        
        public CreateModel(IRecipeService recipeService, ICategoryService categoryService, ILogger<RecipeService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
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
            await _recipeService.CreateAsync(Recipe);

            return RedirectToPage("Index");
        }

        private async Task BuildCategories()
        {
            if (null == _categories)
            {
                _logger.LogDebug("Getting categories");
                _categories = await _categoryService.GetCategories();
                _logger.LogDebug("Received {0} categories from the service.", _categories.Count());
            }
            Categories = new SelectList(_categories, "Id", "Name");
        }
    }
}