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
        
        public string Description { get; set; }

        /// <summary>
        /// List of instructions on how to execute the recipe. Required field.
        /// </summary>
        /// <value></value>
        public List<string> Instructions { get; set; }

        public TimeSpan PrepTime { get; set; }

        public TimeSpan CookTime { get; set; }

        public List<string> Keywords { get; set; }
        
        public string Yield { get; set; }
        
        [Required]
        public List<string> Ingredients { get; set; }

        public List<string> Preparation { get; set; }

        public CategoryViewModel Category { get; set; }

        public bool IsSecret { get; set; }
    }
}