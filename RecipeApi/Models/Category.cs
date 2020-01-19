using System.ComponentModel.DataAnnotations;
using Api.Infrastructure.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeApi.Models
{
    /// <summary>
    /// Represents a recipe category.
    /// </summary>
    public class Category : DataObject
    {
#pragma warning disable CS1591 // Disable xml comment warnings
        
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }
        
        [BsonElement("Description")]
        public string Description { get; set; }
    }
}