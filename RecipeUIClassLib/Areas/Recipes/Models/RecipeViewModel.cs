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
        private TimeSpan _prepTime;
        private TimeSpan _cookTime;

        public string Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }

        /// <summary>
        /// List of instructions on how to execute the recipe. Required field.
        /// </summary>
        /// <value></value>
        public List<string> Instructions { get; set; }

        public string PrepTime 
        { 
            get => GetTimespanString(_prepTime);
        
            set
            {
                if (TimeSpan.TryParseExact(value, "g", null, out var prepTime))
                {
                    _prepTime = prepTime;               
                }
            }
        }

        public string PrepTimeDisplay 
        { 
            get => GetTimespanForDisplay(_prepTime, "Prep Time");
        }

        public string CookTime 
        { 
            get => GetTimespanString(_cookTime);
        
            set
            {
                if (TimeSpan.TryParseExact(value, "g", null, out var cookTime))
                {
                    _cookTime = cookTime;               
                }
            }
        }

        public string CookTimeDisplay 
        { 
            get => GetTimespanForDisplay(_cookTime, "Cook Time");
        }

        public List<string> Keywords { get; set; }
        
        public string Yield { get; set; }
        
        public List<string> Ingredients { get; set; }

        public List<string> Preparation { get; set; }

        public CategoryViewModel Category { get; set; }

        public bool IsSecret { get; set; }

        private string GetTimespanString(TimeSpan timeSpan)
        {
            var minutes = timeSpan.Minutes;
            string minutesDisplay = minutes.ToString();

            if (minutes < 10 && minutes > 0)
            {
                minutesDisplay = $"0{minutes}"; // Add a leading zero
            }
            return $"{timeSpan.Hours}:{minutesDisplay}";
        }

        private string GetTimespanForDisplay(TimeSpan timeSpan, string label)
        {
            var minutes = timeSpan.Minutes;
            var hours = timeSpan.Hours;

            if (minutes > 0 || hours > 0)
            {
                var spanBegin = $"{label}: ";
                var minutesString = minutes == 1 ? "1 minute" : $"{minutes} minutes";
                var hoursString = hours == 1 ? "1 hour" : $"{hours} hours";
                var spanEnd = $"{minutesString}";

                return hours > 0
                    ? $"{spanBegin}{hoursString} and {spanEnd}"
                    : $"{spanBegin}{spanEnd}";
            }
            return string.Empty;
        }
    }
}