using AlbumApi.Models;
using Api.Domain.Controllers;
using Api.Domain.Interfaces;
using Api.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AlbumApi.Controllers
{
    /// <summary>
    /// Manages albums.
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    public class AlbumsController : BaseController<Album<DataObject>>
    {
        /// <summary>
        /// Constructs a album API controller.
        /// </summary>
        /// <param name="albumService"></param>
        /// <param name="logger"></param>
        public AlbumsController(IDataService<Album<DataObject>> albumService, ILogger<AlbumsController> logger)
            : base(albumService, logger)
        {
        }

        /// <summary>
        /// Overridden in order to set the name attribute on HttpGet, the name must be unique per controller.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetAlbum")]
        public override async Task<ActionResult<Album<DataObject>>> GetData(string id)
        {
            return await base.GetData(id);
        }
    }
}
