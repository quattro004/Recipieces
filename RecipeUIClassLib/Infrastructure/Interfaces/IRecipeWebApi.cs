using RecipeUIClassLib.Areas.Recipes.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeUIClassLib.Infrastructure.Interfaces
{
    /// <summary>
    /// Defines the functionality for managing recipes
    /// </summary>
    public interface IRecipeWebApi
    {
        [Get("")]
        Task<IReadOnlyCollection<RecipeViewModel>> ListAsync();

        [Get("/{key}")]
        Task<RecipeViewModel> GetAsync(string key);

        [Delete("/{key}")]
        Task DeleteAsync(string key);

        [Post("")]
        Task CreateAsync([Body] RecipeViewModel data);

        [Put("/{key}")]
        Task UpdateAsync(string key, [Body] RecipeViewModel data);
    }
}
