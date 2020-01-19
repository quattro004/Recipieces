using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlbumUIClassLib.Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlbumUIClassLib.Areas.Pictures.Models
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IPictureService _pictureService;

        public IEnumerable<PictureViewModel> Pictures { get; set; }

        public IndexModel(IPictureService pictureService)
        {
            _pictureService = pictureService ?? throw new ArgumentNullException(nameof(pictureService));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Pictures = await _pictureService.ListAsync();

            return Page();
        }
    }
}