using Microsoft.AspNetCore.Mvc;
using RecipiecesWeb.Models;

namespace RecipiecesWeb.Views.Shared.Components.AlertControl
{
    /// <summary>
    /// Use to control alerts between client and server.
    /// </summary>
    public class AlertControlViewComponent : ViewComponent
    {
        /// <summary>
        /// Displays a <see cref="AlertControlViewComponent" /> for a specific <paramref cref="kindOfAlert" />
        ///  displaying the <paramref cref="title" /> in bold alongside the <paramref cref="message" />.
        /// </summary>
        /// <param name="kindOfAlert">Accepts Bootstrap alerts: success, info, warning, or danger. </param>
        /// <param name="title">Title of the alert e.g. Lookout</param>
        /// <param name="message">Message of the alert e.g. OMG something awesome is happening!</param>
        public IViewComponentResult Invoke(string kindOfAlert, string title, string message)
        {
            return View (new AlertControlViewModel
            {
                KindOfAlert = kindOfAlert,
                Title = title,
                Message = message
            });
        }
    }
}