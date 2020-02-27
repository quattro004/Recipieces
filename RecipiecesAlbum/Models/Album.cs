using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Infrastructure.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipiecesAlbum.Models
{
    /// <summary>
    /// Represents an album which is a container for media objects like pictures, recipes, videos, etc.
    /// </summary>
    public class Album<T> : DataObject where T : DataObject, new()
    {
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }
        
        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Contents")]
        public IEnumerable<T> Contents { get; set; }
    }
}