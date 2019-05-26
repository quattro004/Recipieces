using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecipeUIClassLib.Areas.Recipes.Services;
using RecipeUIClassLib.Extensions;

namespace RecipeUIClassLib.Areas.Recipes.Models
{
    [Authorize]
    public class RecipeModel : PageModel
    {
        protected readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;
        protected readonly ILogger _logger;
        protected IEnumerable<CategoryViewModel> _categories;

        [BindProperty]
        public RecipeViewModel Recipe { get; set; }

        public SelectList Categories { get; set; }

        [BindProperty]
        public string SelectedCategory { get; set; }

        [BindProperty]
        public string Preparation { get; set; }

        [BindProperty]
        public string Ingredients { get; set; }

        [BindProperty]
        public string Keywords { get; set; }

        [BindProperty]
        public string Instructions { get; set; }

        [BindProperty]
        public int CookTimeHours { get; set; }

        [BindProperty]
        public int CookTimeMinutes { get; set; }

        [BindProperty]
        public int CookTimeSeconds { get; set; }

        [BindProperty]
        public int PrepTimeHours { get; set; }

        [BindProperty]
        public int PrepTimeMinutes { get; set; }

        [BindProperty]
        public int PrepTimeSeconds { get; set; }

        public RecipeModel(IRecipeService recipeService, ICategoryService categoryService, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        protected async Task BuildCategories()
        {
            if (null == _categories)
            {
                _logger.LogDebug("Getting categories");
                _categories = await _categoryService.GetCategories();
                _logger.LogDebug("Received {0} categories from the service.", _categories.Count());
            }
            Categories = new SelectList(_categories, "Id", "Name");
        }

        protected void ValidateRecipe()
        {
            if (null == SelectedCategory)
            {
                ModelState.AddModelError("SelectedCategory", "Please select a category");
            }
            if (null == Recipe.Instructions || !Recipe.Instructions.Any())
            {
                ModelState.AddModelError("Instructions", "Please add at least one cooking instruction");                
            }
            if (null == Recipe.Ingredients || !Recipe.Ingredients.Any())
            {
                ModelState.AddModelError("Ingredients", "Please add at least one ingredient");                
            }
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            _logger.LogDebug("Posting a recipe to the API");
            _logger.LogDebug("Cat is {0}", SelectedCategory);
            
            await BuildCategories();
            Recipe.Instructions = Instructions.FromDelimited();
            Recipe.Ingredients = Ingredients.FromDelimited();
            Recipe.Preparation = Preparation.FromDelimited();
            Recipe.Keywords = Keywords.FromDelimited();
            Recipe.CookTime = new TimeSpan(CookTimeHours, CookTimeMinutes, CookTimeSeconds);
            Recipe.PrepTime = new TimeSpan(PrepTimeHours, PrepTimeMinutes, PrepTimeSeconds);
            ValidateRecipe();
            return Page();
        }
    }
}