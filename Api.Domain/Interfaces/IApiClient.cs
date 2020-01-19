using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces
{
    /// <summary>
    /// Defines the basic functionality of the API clients.
    /// </summary>
    public interface IApiClient<T>
    {
         Task<IEnumerable<T>> ListAsync();
         Task CreateAsync(T data);
         Task UpdateAsync(T data);
         Task<T> GetAsync(string id);
         Task DeleteAsync(string id);
    }
}