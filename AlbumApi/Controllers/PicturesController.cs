using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AlbumApi.Models;
using Infrastructure.Controllers;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AlbumApi.Controllers
{
    /// <summary>
    /// Manages pictures. An album must be created first.
    /// </summary>
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
            return await base.GetData(id);
        }
    }
}
