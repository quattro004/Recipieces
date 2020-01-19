using Api.Domain.Controllers;
using Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeApi.Models;
using System.Threading.Tasks;

namespace RecipeApi.Controllers
{
    /// <summary>
    /// Manages recipe categories.
    /// </summary>
    [Route("[controller]")]
    public class CategoriesController : BaseController<Category>
    {
        /// <summary>
        /// Constructs a <see cref="CategoriesController" />
        /// </summary>
        /// <param name="categoryService"></param>
        /// <param name="logger"></param>
        public CategoriesController(IDataService<Category> categoryService, ILogger<CategoriesController> logger)
            : base(categoryService, logger)
        {
        }

        /// <summary>
        /// Overridden in order to set the name attribute on HttpGet, the name must be unique per controller.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        public override async Task<ActionResult<Category>> GetData(string id)
        {
            return await base.GetData(id);
        }
    }
}