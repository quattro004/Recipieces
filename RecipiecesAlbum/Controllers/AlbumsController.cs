using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Controllers;
using Api.Domain.Interfaces;
using Api.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipiecesAlbum.Models;

namespace RecipiecesAlbum.Controllers
{
    /// <summary>
    /// Manages albums.
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    public class AlbumsController : BaseController<Album<DataObject>>
    {
        /// <summary>
        /// Constructs an album API controller.
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

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<Album<DataObject>>>> List()
        {
            return await Task.FromResult(new List<Album<DataObject>>()
            {
                new Album<DataObject>()
                {
                    Name = "Pictures",
                    Description = "Pictures of the Hodge family",
                    CreatedOn = DateTime.Now,
                    Id = Guid.NewGuid().ToString()
                },
                new Album<DataObject>()
                {
                    Name = "Videos",
                    Description = "Videos of the Hodge family",
                    CreatedOn = DateTime.Now,
                    Id = Guid.NewGuid().ToString()
                },
                new Album<DataObject>() 
                {
                    Name = "Music",
                    Description = "Beautiful music of the Hodge family",
                    CreatedOn = DateTime.Now,
                    Id = Guid.NewGuid().ToString()
                }
            });
        }
    }
}
