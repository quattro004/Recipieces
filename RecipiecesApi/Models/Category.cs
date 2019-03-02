using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipiecesApi.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }
        
        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("CreatedOn")]
        public string CreatedOn { get; set; }

        [BsonElement("ModifiedOn")]
        public string ModifiedOn { get; set; }
    }
}