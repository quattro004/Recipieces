using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeUIClassLib.Areas.Recipes.Models;

namespace RecipeUIClassLib.Areas.Recipes.Services
{
    public interface ICategoryService
    {
         Task<IEnumerable<CategoryViewModel>> List();
    }
}