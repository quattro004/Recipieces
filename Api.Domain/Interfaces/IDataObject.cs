using System;

namespace Api.Domain.Interfaces
{
    /// <summary>
    /// Defines a data object which is used for CRUD.
    /// </summary>
    public interface IDataObject
    {
        string Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Determines whether this object was created or updated in the data store.
        /// </summary>
        bool DoesNotExist { get; set; }
    }
}