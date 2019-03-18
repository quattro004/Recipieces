using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeUIClassLib.Areas.Recipes.Models
{
    /// <summary>
    /// Represents a recipe category in the UI.
    /// </summary>
    public class CategoryViewModel
    {
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}