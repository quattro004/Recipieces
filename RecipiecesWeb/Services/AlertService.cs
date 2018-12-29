using RecipiecesWeb.Models;

namespace RecipiecesWeb.Services
{
    /// <summary>
    /// Manages alerts for the web app.
    /// </summary>
    public class AlertService : IAlertService
    {
        public AlertService ()
        {
            AlertData = new AlertControlViewModel();
        }

        /// <summary>
        /// Alert data last set in SetupAlert().
        /// </summary>
        /// <value></value>
        public AlertControlViewModel AlertData { get; set; }

    }
}