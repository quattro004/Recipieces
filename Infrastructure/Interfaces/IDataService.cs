using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    /// <summary>
    /// Defines the functionality of the data services.
    /// </summary>
    public interface IDataService<T>
    {
        /// <summary>
        /// Gets a list of data objects
        /// </summary>
        /// <returns>List of data</returns>
        Task<IEnumerable<T>> List();
        
        /// <summary>
        /// Gets a data object by <paramref name="id" />
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetData(string id);
        
        /// <summary>
        /// Creates a data object asynchronously.
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns>Newly created data object or null if it already exists.</returns>
        /// <exception type="ArgumentNullException">Thrown when the <paramref name="dataObject" /> is null.</exception>
        Task<T> CreateAsync(T dataObject);

        /// <summary>
        /// Updates a data object by replacing it.
        /// </summary>
        /// <param name="id">T identifier.</param>
        /// <param name="dataIn">Data object to update.</param>
        /// <returns>1 if successful 0 otherwise.</returns>
        Task<long> Update(string id, T dataIn);

        /// <summary>
        /// Removes a data object.
        /// </summary>
        /// <param name="dataIn">Data object to remove.</param>
        /// <returns>1 if successful 0 otherwise.</returns>
        Task<long> Remove(T dataIn);

        /// <summary>
        /// Removes a data object by <paramref name="id" />.
        /// </summary>
        /// <param name="id">Data object's identifier.</param>
        /// <returns>1 if successful 0 otherwise.</returns>
        Task<long> Remove(string id);
    }
}
