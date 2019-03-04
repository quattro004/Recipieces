using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeApi.Models
{
    /// <summary>
    /// Represents a recipe.
    /// </summary>
    public class Recipe : DataObject
    {
#pragma warning disable CS1591 // Disable xml comment warnings

        [Required]
        [BsonElement("Title")]
        public string Title { get; set; }
        
        [Required]
        [BsonElement("Description")]
        public string Description { get; set; }

        [Required]
        [BsonElement("Instructions")]
        public List<string> Instructions { get; set; }

        [BsonElement("PrepTime")]
        public TimeSpan PrepTime { get; set; }

        [BsonElement("CookTime")]
        public TimeSpan CookTime { get; set; }

        [BsonElement("Keywords")]
        public List<string> Keywords { get; set; }
        
        [BsonElement("Yield")]
        public string Yield { get; set; }
        
        [BsonElement("Ingredients")]
        public List<string> Ingredients { get; set; }

        [BsonElement("Preparation")]
        public string Preparation { get; set; }

        [BsonElement("Category")]
        public Category Category { get; set; }
    }
}