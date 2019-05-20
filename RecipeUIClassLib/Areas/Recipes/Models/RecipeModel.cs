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

        protected void BuildInstructions()
        {
            // TODO: need to make required field
            //
            // The instructions end up as one string with new lines. Create a list from it.
            _logger.LogDebug("Building instructions: {0}", Instructions);
            var instructionList = Instructions.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            // Remove existing and add from current list
            Recipe.Instructions = new List<string>(instructionList);
        }
    }
}