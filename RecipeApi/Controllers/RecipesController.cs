using System.Threading.Tasks;
using Api.Domain.Controllers;
using Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeApi.Models;

namespace RecipeApi.Controllers
{
    /// <summary>
    /// Manages recipes.
    /// </summary>
    [Route("[controller]")]
    public class RecipesController : BaseController<Recipe>
    {
        /// <summary>
        /// Constructs a recipe API controller.
        /// </summary>
        /// <param name="recipeService"></param>
        /// <param name="logger"></param>
        public RecipesController(IDataService<Recipe> recipeService, ILogger<RecipesController> logger)
            : base(recipeService, logger)
        {
        }

        /// <summary>
        /// Overridden in order to set the name attribute on HttpGet, the name must be unique per controller.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetRecipe")]
        public override async Task<ActionResult<Recipe>> GetData(string id)
        {
            return await base.GetData(id);
        }
    }
}