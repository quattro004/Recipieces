using System;
using Api.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Infrastructure.Models
{
    /// <summary>
    /// Defines a data object which is used for CRUD.
    /// </summary>
    public class DataObject : IDataObject
    {
        /// <summary>
        /// NoSql database identifier, currently using Mongodb
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The date/time this object was created.
        /// </summary>
        [BsonElement("CreatedOn")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// The date/time this object was modified.
        /// </summary>
        [BsonElement("ModifiedOn")]
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Determines whether this object was created or updated in the data store.
        /// </summary>
        [BsonIgnore]
        public bool DoesNotExist { get; set; }

        // TODO: is this necessary?
        //
        /// <summary>
        /// Null object pattern. Use when a object can't be created or updated in the data store.
        /// </summary>
        /// <value></value>
        [BsonIgnore]
        public static DataObject NotCreated { get => notCreated; set => notCreated = value; }

        private static DataObject notCreated = new DataObject
        {
            Id = string.Empty,
            DoesNotExist = true
        };
    }
}