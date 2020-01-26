using RecipeUIClassLib.Areas.Recipes.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeUIClassLib.Infrastructure.Interfaces
{
    /// <summary>
    /// Defines the functionality for getting recipe categories.
    /// </summary>
    public interface ICategoryWebApi
    {
        [Get("")]
        Task<IReadOnlyCollection<CategoryViewModel>> ListAsync();

        [Get("/{key}")]
        Task<CategoryViewModel> GetAsync(string key);

    }
}
