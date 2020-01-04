using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AlbumApi.Models
{
    /// <summary>
    /// Represents an album. An album is a container for things like photos, video, etc.
    /// </summary>
    public class Album<T> : DataObject where T : DataObject
    {
#pragma warning disable CS1591 // Disable xml comment warnings
        
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }
        
        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Contents")]
        public IEnumerable<T> Contents { get; set; }
    }
}