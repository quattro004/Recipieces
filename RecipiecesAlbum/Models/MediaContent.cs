using Api.Infrastructure.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecipiecesAlbum.Models
{
    /// <summary>
    /// Represents album media content like videos, pictures, music.
    /// </summary>
    public class MediaContent : DataObject
    {
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("DateTaken")]
        public DateTime? DateTaken { get; set; }

        public Stream Data { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
