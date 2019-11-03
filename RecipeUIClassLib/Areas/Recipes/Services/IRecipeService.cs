using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeUIClassLib.Areas.Recipes.Models;

namespace RecipeUIClassLib.Areas.Recipes.Services
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