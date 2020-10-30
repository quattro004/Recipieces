using Api.Infrastructure.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipiecesWeb.Models
{
    /// <summary>
    /// Represents album media content like videos, pictures, music.
    /// </summary>
    public class MediaContent : DataObject
    {
        /// <summary>
        /// Name of the content to display in the UI.
        /// </summary>
        [BsonElement("Name")]
        public string Name {get; set; }

        /// <summary>
        /// Physical path to the content.
        /// </summary>
        [BsonElement("Path")]
        public string Path { get; set; }
    }
}
