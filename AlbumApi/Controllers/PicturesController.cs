using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AlbumApi.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Api.Domain.Controllers;
using Api.Domain.Interfaces;

namespace AlbumApi.Controllers
{
    /// <summary>
    /// Manages pictures. An album must be created first.
    /// </summary>
    [Produces("application/json")]
    [Route("albums/{albumId}/[controller]")]
    public class PicturesController : BaseController<Album<Picture>>
    {
        /// <summary>
        /// Constructs a picture album API controller.
        /// </summary>
        /// <param name="pictureService"></param>
        /// <param name="logger"></param>
        public PicturesController(IDataService<Album<Picture>> pictureService, ILogger<PicturesController> logger)
            : base(pictureService, logger)
        {
        }

        /// <summary>
        /// Overridden in order to set the name attribute on HttpGet, the name must be unique per controller.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetPicture")]
        public override async Task<ActionResult<Album<Picture>>> GetData(string id)
        {
            return await Task.FromResult(new Album<Picture>()
            {
                Contents = new List<Picture>
                {
                    new Picture { Id = Guid.NewGuid().ToString(), Name = "Test Picture"}
                }
            });
           // return await base.GetData(id);
        }
    }
}
