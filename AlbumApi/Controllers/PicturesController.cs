using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AlbumApi.Models;

namespace AlbumApi.Controllers
{
    /// <summary>
    /// Manages pictures. An album must be created first.
    /// </summary>
    [Produces("application/json")]
    [Route("albums/{albumId}/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        /// <summary>
        /// Gets a picture album.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Album<Picture>> Get()
        {
            return new Album<Picture>
            {
                Id = "42",
                Name = "Kids",
                Description = "Pictures of the kids",
                Contents = new List<Picture>
                {
                    new Picture { Name = "Xavier", Id = "23e32", DateTaken = DateTime.Now.AddDays(-21) },
                    new Picture { Name = "Xander", Id = "23e343", DateTaken = DateTime.Now.AddDays(-13) }
                },

            };
        }

        // GET: api/Picture/5
        [HttpGet("{id}", Name = "Get_Pictures")]
        public ActionResult<Picture> Get(int id)
        {
            return Ok(new Picture { Name = "Xavier", Id = "23e32", DateTaken = DateTime.Now.AddDays(-21) });
        }

        // POST: api/Picture
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Picture/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
