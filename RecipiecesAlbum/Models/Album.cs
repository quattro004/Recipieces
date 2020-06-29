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
        public Album()
        {
            Contents = new List<T>();
        }

        /// <summary>
        /// Name of the album.
        /// </summary>
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Description of the album.
        /// </summary>
        [BsonElement("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Contents of the album.
        /// </summary>
        [BsonElement("Contents")]
        public IList<T> Contents { get; set; }
    }
}