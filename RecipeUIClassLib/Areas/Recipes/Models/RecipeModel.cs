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
        public string Instructions { get; set; }

        [BindProperty]
        public string Preparation { get; set; }

        [BindProperty]
        public string Ingredients { get; set; }

        [BindProperty]
        public string Keywords { get; set; }

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

        /// <summary>
        /// Builds up the Instructions, Ingredients, Keywords and Preparation (if present) lists from the new line delimited
        /// text areas in the view.
        /// </summary>
        protected void BuildLists()
        {
            //
            // The instructions, ingredients, keywords and preparation end up as one string with new lines in the view.
            // Create a list from it.
            _logger.LogDebug("Building instructions: {0}", Instructions);
            Recipe.Instructions = BuildList(Instructions);

            _logger.LogDebug("Building ingredients: {0}", Ingredients);
            Recipe.Ingredients = BuildList(Ingredients);
            
            _logger.LogDebug("Building preparation: {0}", Preparation);
            Recipe.Preparation = BuildList(Preparation);

            _logger.LogDebug("Building keywords: {0}", Keywords);
            Recipe.Keywords= BuildList(Keywords);
        }

        private List<string> BuildList(string delimitedString)
        {
            if (!string.IsNullOrWhiteSpace(delimitedString))
            {
                return new List<string>(delimitedString.Split(new string[] {"\r\n"}, 
                    StringSplitOptions.RemoveEmptyEntries));
            }
            return new List<string>();
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
    }
}