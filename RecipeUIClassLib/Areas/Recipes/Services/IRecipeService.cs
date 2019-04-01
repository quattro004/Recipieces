using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeUIClassLib.Areas.Recipes.Models;

namespace RecipeUIClassLib.Areas.Recipes.Services
{
    public interface IRecipeService
    {
         Task<IEnumerable<RecipeViewModel>> GetRecipesAsync();

         Task CreateAsync(RecipeViewModel recipe);
    }
}