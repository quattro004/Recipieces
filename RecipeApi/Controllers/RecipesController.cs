using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeApi.Models;
using Infrastructure.Services;

namespace RecipeApi.Controllers
{
    /// <summary>
    /// Manages recipes.
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly DataService<Recipe> _recipeService;
        private readonly ILogger<RecipesController> _logger;

        /// <summary>
        /// Constructs a <see cref="RecipesController" />
        /// </summary>
        /// <param name="recipeService"></param>
        /// <param name="logger"></param>
        public RecipesController(DataService<Recipe> recipeService, ILogger<RecipesController> logger)
        {
            _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets all recipes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Recipe> List()
        {
            _logger.LogDebug("Getting all recipes");
            return _recipeService.List();
        }

        /// <summary>
        /// Gets a recipe by <paramref name="id" />.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="Recipe" /> if one exists by the identifier.</returns>
        [HttpGet("{id:length(24)}", Name = "GetRecipe")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public ActionResult<Recipe> Get(string id)
        {
            _logger.LogDebug("Getting a recipe with id {0}", id);
            var recipe = _recipeService.Get(id);
            if (recipe.DoesNotExist)
            {
                return NotFound();
            } 
            
            return recipe;
        }

        /// <summary>
        /// Creates a new recipe.
        /// </summary>
        /// <param name="recipe"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Recipe
        ///     {
        ///         "title": "Chicken Fried Steak",
        ///         "description": "It's the best ever!",
        ///         "instructions": ["Do work", "Be awesome", "Eat Food"], 
        ///         "prepTime": "10 minutes",
        ///         "cookTime": "30 minutes",
        ///         "keywords": ["Chicken", "Steak"],
        ///         "yield": "Serves 10",
        ///         "ingredients": ["Chicken", "Skirt steak", "Salt", "Pepper"],
        ///         "preparation": "",
        ///         "category": 
        ///         {
        ///             "id": "5c7aedd2f41b837001a763ff",
        ///             "name": "Dinners"
        ///         }
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync(Recipe recipe)
        {
            _logger.LogDebug("Creating a recipe titled {0}", recipe.Title);
            recipe = await _recipeService.CreateAsync(recipe);
            
            return CreatedAtRoute("GetRecipe", new { id = recipe.Id }, recipe);
        }

        /// <summary>
        /// Updates an existing recipe.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recipeIn"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Recipe
        ///     {
        ///         "title": "Pancakes",
        ///         "description": "Just like yo mama made",
        ///         "instructions": [
        ///           "Combine dry together",
        ///           "Beat egg in a bowl and add 1 cup milk and butter",
        ///           "Add 1/4 cup more milk if needed"
        ///         ],
        ///         "prepTime": "00:06:00",
        ///         "cookTime": "00:30:00",
        ///         "keywords": [
        ///           "Pancakes",
        ///           "Mama's best"
        ///         ],
        ///         "yield": "Makes 10-12 4 inch pancakes",
        ///         "ingredients": [
        ///           "1 1/2 cup flour",
        ///           "1 tbsp baking powder",
        ///           "1/2 tbsp baking soda",
        ///           "2 tbsp sugar",
        ///           "3/4 tsp salt",
        ///           "1 tsp vanilla",
        ///           "1 1/ 4 cup milk",
        ///           "1 egg",
        ///           "2 tbsp melted butter"
        ///         ],
        ///         "preparation": [
        ///           "Melt butter in a container"
        ///         ],
        ///         "category": {
        ///           "name": "Breakfast",
        ///           "description": null,
        ///           "id": "5d2a5c76eb06ed0001ae0b7a"
        ///         },
        ///         "id": "5d2a5f7deb06ed0001ae0b7b"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(string id, Recipe recipeIn)
        {
            _logger.LogDebug("Updating a recipe with id {0}", id);
            if (id != recipeIn.Id)
            {
                _logger.LogError("The recipe's id {0} doesn't match the route's id {1}", recipeIn.Id,
                    id);
                return NotFound();
            }
            var updatedCount = await _recipeService.Update(id, recipeIn);

            if (updatedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing recipe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogDebug("Deleting a recipe with id {0}", id);
            var deletedCount = await _recipeService.Remove(id);
            
            if (deletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}