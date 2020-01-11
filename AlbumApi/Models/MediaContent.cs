using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using System.IO;
using System;

namespace AlbumApi.Models
{
    /// <summary>
    /// Represents album media content like videos, pictures, music.
    /// </summary>
    public class MediaContent : DataObject
    {
#pragma warning disable CS1591 // Disable xml comment warnings
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("DateTaken")]
        public DateTime? DateTaken { get; set; }

        public Stream Data { get; set; }
    }
}
