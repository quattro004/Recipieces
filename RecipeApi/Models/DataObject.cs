using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeApi.Models
{
    /// <summary>
    /// Defines a data object which is used for CRUD.
    /// </summary>
    public class DataObject
    {
#pragma warning disable CS1591 // Disable xml comment warnings
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
       [BsonElement("CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [BsonElement("ModifiedOn")]
        public DateTime ModifiedOn { get; set; }

#pragma warning restore CS1591 // Restore xml comment warnings
       
        /// <summary>
        /// Determines whether this object was created or updated in the data store.
        /// </summary>
        [BsonIgnore]
        public bool DoesNotExist { get; set; }

        /// <summary>
        /// Null object pattern. Use when a object can't be created or updated in the data store.
        /// </summary>
        /// <value></value>
        [BsonIgnore]
        public static DataObject NotCreated = new DataObject
        {
            Id = string.Empty,
            DoesNotExist = true
        };
    }
}