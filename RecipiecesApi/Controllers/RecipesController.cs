using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipiecesApi.Models;

namespace RecipiecesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(ILogger<RecipesController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("Getting all recipes...");

            return Ok(new List<Recipe>
            {
                new Recipe
                {
                    Id = 1,
                    Title = "Cheesy Mac",
                    Description = "This is the best cheesy mac!"
                },
                new Recipe
                {
                    Id = 2,
                    Title = "Fried chicken",
                    Description = "This is the best chick!"
                }
            });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 1)
            {
                _logger.LogDebug("Found recipe with id {0}", id);

                return Ok(new Recipe
                {
                    Id = 1,
                    Title = "Cheesy Mac",
                    Description = "This is the best cheesy mac!"
                });
            }

            _logger.LogWarning("Dude, recipe with id {0} appears to be missing!", id);
            
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
