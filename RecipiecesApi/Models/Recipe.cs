using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipiecesApi.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("Title")]
        public string Title { get; set; }
        
        [BsonElement("Description")]
        public string Description { get; set; }
    }
}