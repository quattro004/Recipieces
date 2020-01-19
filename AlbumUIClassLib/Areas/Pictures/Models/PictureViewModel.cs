using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    /// <summary>
    /// Represents a picture in the UI.
    /// </summary>
    public class PictureViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateTaken { get; set; }
    }
}