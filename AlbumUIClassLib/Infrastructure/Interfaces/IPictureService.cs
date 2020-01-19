using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace  AlbumUIClassLib.Infrastructure.Interfaces
{
    /// <summary>
    /// Defines the functionality of the picture service.
    /// </summary>
    public interface IPictureService
    {
        /// <summary>
        /// Gets a list of all the pictures.
        /// </summary>
        /// <returns>List of <see cref="PictureViewModel" />.</returns>
        Task<IEnumerable<PictureViewModel>> ListAsync();
    }
}