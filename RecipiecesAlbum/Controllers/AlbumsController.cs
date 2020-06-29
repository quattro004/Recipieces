using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Controllers;
using Api.Domain.Interfaces;
using Api.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using RecipiecesAlbum.Filters;
using RecipiecesAlbum.Helpers;
using RecipiecesAlbum.Models;

namespace RecipiecesAlbum.Controllers
{
    /// <summary>
    /// Manages albums.
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".gif", ".png" };
        private readonly string _targetFolderPath;
        private readonly ILogger _logger;
        // Get the default form options so that we can use them to set the default 
        // limits for request body data.
        private readonly FormOptions _defaultFormOptions;
        private readonly IDataService<Album<MediaContent>> _albumService;


        /// <summary>
        /// Constructs an album API controller.
        /// </summary>
        /// <param name="albumService"></param>
        /// <param name="logger"></param>
        public AlbumsController(IDataService<Album<MediaContent>> albumService, ILogger<AlbumsController> logger, IConfiguration config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _albumService = albumService ?? throw new ArgumentNullException(nameof(albumService));
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            // Save physical files to a path provided by configuration:
            _targetFolderPath = config.GetValue<string>("AlbumContentPath");
            _defaultFormOptions = new FormOptions();
            _defaultFormOptions.MultipartBodyLengthLimit = 32768;
        }

        /// <summary>
        /// Creates a new album.
        /// </summary>
        /// <param name="dataObject"></param>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync(Album<MediaContent> dataObject)
        {
            if (dataObject == DataObject.NotCreated)
            {
                ModelState.AddModelError("Create", $"The {nameof(dataObject)} cannot be null!");

                return BadRequest(ModelState);
            }
            var newDataObject = await _albumService.CreateAsync(dataObject);

            return CreatedAtRoute(nameof(Get), new { id = newDataObject.Id }, newDataObject);
        }

        /// <summary>
        /// Overridden in order to set the name attribute on HttpGet, the name must be unique per controller.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name="GetAlbum")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Album<MediaContent>>> Get(string id)
        {
            return Ok(await _albumService.GetDataAsync(id));
        }

         /// <summary>
        /// Gets all data objects as a list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public virtual async Task<ActionResult<IEnumerable<Album<MediaContent>>>> List()
        {
            return Ok(await _albumService.ListAsync());
        }

        /// <summary>
        /// Updates an album's contents.
        /// </summary>
        [HttpPost("UpdateContents", Name="UpdateContents")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UpdateContents()
        {
            var contentType = Request.ContentType;

            if (!MultipartRequestHelper.IsMultipartContentType(contentType))
            {
                var errorMessage = $"The request couldn't be processed (Error 1).";
                ModelState.AddModelError("File", errorMessage);
                _logger.LogError(errorMessage);

                return BadRequest(ModelState);
            }

            var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(contentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();
            Album<MediaContent> album = null;

            while (section != null)
            {
                var hasContentDispositionHeader = 
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (contentDisposition.Name == "albumId")
                    {
                        using (var streamReader = new StreamReader(section.Body))
                        {
                            var albumId = await streamReader.ReadToEndAsync();
                            // TODO: update when service is ready
                            album = new Album<MediaContent>(); //await _albumService.GetDataAsync(albumId);

                            if (album == DataObject.NotCreated)
                            {
                                var errorMessage = $"The request couldn't be processed (Error 2).";
                                ModelState.AddModelError("AlbumId", errorMessage);
                                _logger.LogError(errorMessage);

                                return BadRequest(ModelState);
                            }
                        }
                    }
                    // The HasFileContentDisposition check assumes that there's a file present without form data.
                    // If form data is present, this method immediately fails and returns the model error. 
                    else if (!MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        var errorMessage = $"The request couldn't be processed (Error 3).";
                        ModelState.AddModelError("File", errorMessage);
                        _logger.LogError(errorMessage);

                        return BadRequest(ModelState);
                    }
                    else
                    {
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                                contentDisposition.FileName.Value);
                        var trustedFileNameForFileStorage = Path.GetRandomFileName();

                        // **WARNING!**
                        // In the following example, the file is saved without
                        // scanning the file's contents. In most production
                        // scenarios, an anti-virus/anti-malware scanner API
                        // is used on the file before making the file available
                        // for download or for use by other systems. 
                        // For more information, see the topic that accompanies 
                        // this sample.

                        var streamedFileContent = await FileHelpers.ProcessStreamedFile(section, contentDisposition,
                            ModelState,  _permittedExtensions, _fileSizeLimit);

                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }
                        var filePath = Path.Combine(_targetFolderPath, trustedFileNameForFileStorage);

                        using (var targetStream = System.IO.File.Create(filePath))
                        {
                            await targetStream.WriteAsync(streamedFileContent);

                            _logger.LogInformation(
                                $"Uploaded file '{trustedFileNameForDisplay}' saved to " +
                                $"'{_targetFolderPath}' as {trustedFileNameForFileStorage}", 
                                trustedFileNameForDisplay, _targetFolderPath, 
                                trustedFileNameForFileStorage);
                            album.Contents.Add(new MediaContent 
                            {
                                Name = trustedFileNameForDisplay,
                                Path = filePath
                            });

                            await _albumService.Update(album.Id, album);
                        }
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

           return NoContent();
        }
    }
}
