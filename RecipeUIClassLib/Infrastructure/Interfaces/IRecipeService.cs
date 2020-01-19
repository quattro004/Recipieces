using RecipeUIClassLib.Areas.Recipes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeUIClassLib.Infrastructure.Interfaces
{
    public interface IRecipeService
    {
         Task<IEnumerable<RecipeViewModel>> ListAsync();
         Task CreateAsync(RecipeViewModel recipe);
         Task UpdateAsync(RecipeViewModel recipe);
         Task<RecipeViewModel> GetAsync(string id);
         Task DeleteAsync(string id);
    }
}