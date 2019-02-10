using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RecipiecesWeb.Models;

namespace RecipiecesWeb.Services
{
    /// <summary>
    /// Manages alerts for the web app.
    /// </summary>
    public class AlertService : IAlertService
    {
        private readonly ILogger<AlertService> _logger;

        public AlertService(ILogger<AlertService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            AlertData = new AlertControlViewModel();
        }

        /// <summary>
        /// Alert data last set in SetupAlert().
        /// </summary>
        /// <value></value>
        public AlertControlViewModel AlertData { get; set; }

        /// <summary>
        /// Clears any existing alert data.
        /// </summary>
        public Task ClearAlert()
        {
            // TODO: try using an action
            return new Task(() =>
            {
                if (AlertData != null)
                {
                    _logger.LogDebug("Clearing the alert service alert data.");
                    AlertData.KindOfAlert = AlertData.Message = AlertData.Title = string.Empty;
                }
            });
        }

        /// <summary>
        /// Sets the alert data to display.
        /// </summary>
        /// <param name="title">Title of the alert.</param>
        /// <param name="message">Message to the user.</param>
        /// <param name="kindOfAlert">Kind of alert, accepts Bootstrap alerts:
        /// success, info, warning, or danger. Defaults to info.</param>
        public void SetAlert(string title, string message, string kindOfAlert = "info")
        {
            _logger.LogDebug("Setting alert");
            
            AlertData = new AlertControlViewModel
            {
                Title = title,
                Message = message,
                KindOfAlert = kindOfAlert
            };
        }
    }
}