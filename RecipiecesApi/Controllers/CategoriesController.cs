using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipiecesApi.Models;
using RecipiecesApi.Services;

namespace RecipiecesApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(CategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            _logger.LogDebug("Getting all categories");
            return _categoryService.Get();
        }

        /// <summary>
        /// Gets a category by <paramref cref="id" />.
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

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync(Category category)
        {
            _logger.LogDebug("Creating a category named {0}", category.Name);
            category = await _categoryService.CreateAsync(category);

            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryIn"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public IActionResult Update(string id, Category categoryIn)
        {
            _logger.LogDebug("Updating a category with id {0}", id);
            var category = _categoryService.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryService.Update(id, categoryIn);

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
        public IActionResult Delete(string id)
        {
            _logger.LogDebug("Deleting a category with id {0}", id);
            var category = _categoryService.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryService.Remove(category.Id);

            return NoContent();
        }
    }
}