using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RecipiecesWeb.Services;

namespace RecipiecesWeb.Views.Shared.Components.AlertControl
{
    /// <summary>
    /// Used to clear out the AlertService alert data after a view is rendered.
    /// </summary>
    public class ClearAlertDataFilter : IAsyncResultFilter
    {
        private readonly IAlertService _alertService;

        public ClearAlertDataFilter(IAlertService alertService)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // Clear out any alert messages. This was added because the alert service
            // is a singleton and a result filter provides an easy way to clear any alerts after 
            // a response has been sent to the client.
            if (!context.Cancel)
            {
                context.HttpContext.Response.OnCompleted(async () => 
                { 
                    await _alertService.ClearAlert();
                });
            }

            await next();
        }
    }
}