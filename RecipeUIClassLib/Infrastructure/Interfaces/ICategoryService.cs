using RecipeUIClassLib.Areas.Recipes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeUIClassLib.Infrastructure.Interfaces
{
    /// <summary>
    /// Defines the functionality of the category service.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Gets a list of all the categories.
        /// </summary>
        /// <returns>List of <see cref="CategoryViewModel" />.</returns>
        Task<IEnumerable<CategoryViewModel>> ListAsync();
    }
}