using Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Api.Domain.Controllers
{
    /// <summary>
    /// Contains base CRUD functionality for API controllers.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : IDataObject
    {
        private readonly IDataService<T> _dataService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructs an API controller.
        /// </summary>
        /// <param name="dataService"></param>
        /// <param name="logger"></param>
        protected BaseController(IDataService<T> dataService, ILogger logger)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets all data objects as a list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public virtual async Task<ActionResult<IEnumerable<T>>> List()
        {
            return Ok(await _dataService.ListAsync());
        }

        /// <summary>
        /// Gets a data object by <paramref name="id" />.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="IDataObject" /> if one exists by the identifier.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public virtual async Task<ActionResult<T>> GetData(string id)
        {
            _logger.LogDebug("Getting a data object with id {0}", id);
            var dataObject = await _dataService.GetDataAsync(id);
            if (dataObject.DoesNotExist)
            {
                return NotFound();
            }

            return Ok(dataObject);
        }

        /// <summary>
        /// Creates a new data object.
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> CreateAsync(T dataObject)
        {
            var newDataObject = await _dataService.CreateAsync(dataObject);
            return CreatedAtRoute(nameof(GetData), new { id = newDataObject.Id }, newDataObject);
        }

        /// <summary>
        /// Updates an existing data object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataObject"></param>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public virtual async Task<IActionResult> Update(string id, T dataObject)
        {
            _logger.LogDebug("Updating a data object with id {0}", id);
            if (id != dataObject?.Id)
            {
                _logger.LogError("The data object's id {0} doesn't match the route's id {1}", dataObject.Id,
                    id);
                return NotFound();
            }
            var updatedCount = await _dataService.Update(id, dataObject);

            if (updatedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing data object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public virtual async Task<IActionResult> Delete(string id)
        {
            _logger.LogDebug("Deleting a data object with id {0}", id);
            var deletedCount = await _dataService.Remove(id);

            if (deletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
