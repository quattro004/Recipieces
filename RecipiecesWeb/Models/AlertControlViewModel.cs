namespace RecipiecesWeb.Models
{
    /// <summary>
    /// Used with the AlertControlViewComponent to contain the data in the alert.
    /// </summary>
    public class AlertControlViewModel
    {
        /// <summary>
        /// Accepts Bootstrap alerts: success, info, warning, or danger.
        /// </summary>
        /// <value></value>
        public string KindOfAlert { get; set; }

        /// <summary>
        /// Title of the alert e.g. Lookout
        /// </summary>
        /// <value></value>
        public string Title { get; set; }
        
        /// <summary>
        /// Message of the alert e.g. OMG something awesome is happening! 
        /// </summary>
        /// <value></value>
        public string Message { get; set; }

    }
}