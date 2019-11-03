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
    /// Manages recipe categories.
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataService<Category> _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        /// <summary>
        /// Constructs a <see cref="CategoriesController" />
        /// </summary>
        /// <param name="categoryService"></param>
        /// <param name="logger"></param>
        public CategoriesController(DataService<Category> categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Category> List()
        {
            _logger.LogDebug("Getting all categories");
            return _categoryService.List();
        }

        /// <summary>
        /// Gets a category by <paramref name="id" />.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="Category" /> if one exists by the identifier.</returns>
        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public ActionResult<Category> Get(string id)
        {
            _logger.LogDebug("Getting a category with id {0}", id);
            var category = _categoryService.Get(id);
            if (category.DoesNotExist)
            {
                return NotFound();
            } 
            
            return category;
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="category"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Category
        ///     {
        ///        "name": "Cat1",
        ///        "description": "This is an awesome category!"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync(Category category)
        {
            var catName = category.Name;
            _logger.LogDebug("Creating a category named {0}", catName);
            category = await _categoryService.CreateAsync(category);
            // Check to see if the category was created successfully, a null object pattern is used for duplicates.
            // The name is required so if the client didn't pass one in the framework will handle.
            if (category.DoesNotExist)
            {
                ModelState.AddModelError(nameof(CategoriesController), $"Category {catName} already exists.");
                return BadRequest(ModelState);
            }
            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryIn"></param>
        ///  /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Category
        ///     {
        ///        "name": "Cat2",
        ///        "description": "This is an even awesomer category!"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(string id, Category categoryIn)
        {
            _logger.LogDebug("Updating a category with id {0}", id);
            var updatedCount = await _categoryService.Update(id, categoryIn);

            if (updatedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogDebug("Deleting a category with id {0}", id);
            var deletedCount = await _categoryService.Remove(id);
            
            if (deletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}