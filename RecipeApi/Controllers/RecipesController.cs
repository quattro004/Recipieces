using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeApi.Extensions;
using RecipeApi.Models;
using RecipeApi.Services;

namespace RecipeApi.Controllers
{
    /// <summary>
    /// Manages recipes.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
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
        public IEnumerable<Recipe> Get()
        {
            _logger.LogDebug("Getting all recipes");
            return _recipeService.Get();
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
        ///         "id": "string",
        ///         "title": "string",
        ///         "description": "string",
        ///         "instructions": ["string"], 
        ///         "prepTime": "string",
        ///         "cookTime": "string",
        ///         "keywords": ["string"],
        ///         "yield": "string",
        ///         "ingredients": ["string"],
        ///         "preparation": "string",
        ///         "category": 
        ///         {
        ///             "id": "string",
        ///             "name": "string",
        ///             "description": "string",
        ///             "createdOn": "2019-03-02T12:13:18.909Z",
        ///             "modifiedOn": "2019-03-02T12:13:18.909Z"
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
        ///  /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Recipe
        ///     {
        ///        "name": "Cat2",
        ///        "description": "This is an even awesomer recipe!"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(string id, Recipe recipeIn)
        {
            _logger.LogDebug("Updating a recipe with id {0}", id);
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