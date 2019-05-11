using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeUIClassLib.Areas.Recipes.Models
{
    /// <summary>
    /// Represents a recipe in the UI.
    /// </summary>
    public class RecipeViewModel
    {
        public string Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public List<string> Instructions { get; set; }

        public TimeSpan PrepTime { get; set; }

        public TimeSpan CookTime { get; set; }

        public List<string> Keywords { get; set; }
        
        public string Yield { get; set; }
        
        public List<string> Ingredients { get; set; }

        public string Preparation { get; set; }

        public CategoryViewModel Category { get; set; }
    }
}