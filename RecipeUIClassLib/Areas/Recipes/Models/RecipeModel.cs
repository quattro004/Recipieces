using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeUIClassLib.Extensions;
using RecipeUIClassLib.Infrastructure.Interfaces;

namespace RecipeUIClassLib.Areas.Recipes.Models
{
    [Authorize]
    public class RecipeModel : PageModel
    {
        private readonly ICategoryWebApi _categoryClient;
        private IEnumerable<CategoryViewModel> _categories;

        [BindProperty]
        public RecipeViewModel Recipe { get; set; }

        public List<SelectListItem> UiCategories { get; private set; }

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
        
        protected IEnumerable<CategoryViewModel> Categories
        {
            get => BuildCategories().GetAwaiter().GetResult();
        }

        public RecipeModel(ICategoryWebApi categoryClient)
        {
            _categoryClient = categoryClient ?? throw new ArgumentNullException(nameof(categoryClient));
        }

        private async Task<IEnumerable<CategoryViewModel>> BuildCategories()
        {
            _categories ??= await _categoryClient.ListAsync();
            UiCategories ??= (from cat in _categories select new SelectListItem(cat.Name, cat.Id)).ToList();
            return _categories;
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
            if (Recipe.CookTime.Equals(default(TimeSpan)))
            {
                ModelState.AddModelError("CookTime", "Please add the cooking time");                
            }
        }

        protected void BuildRecipe()
        {
            Recipe.Instructions = Instructions.FromDelimited();
            Recipe.Ingredients = Ingredients.FromDelimited();
            Recipe.Preparation = Preparation.FromDelimited();
            Recipe.Keywords = Keywords.FromDelimited();
        }
    }
}