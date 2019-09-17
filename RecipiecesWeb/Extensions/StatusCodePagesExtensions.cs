using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace RecipiecesWeb.Extensions
{
    public static class MyStatusCodePagesExtensions
    {
        // <summary>
        // Adds a StatusCodePages middleware with the given options that checks for responses with status codes
        // between 400 and 599 that do not have a body.
        // </summary>
        // <param name="app"></param>
        // <param name="options"></param>
        // <returns></returns>
        public static IApplicationBuilder UseStatusCodePages(this IApplicationBuilder app, StatusCodePagesOptions options,
            int statusCode)
        {
            return app.UseMiddleware<MyStatusCodePagesMiddleware>(options, statusCode);
	    }

        /// <summary>
        /// Adds a StatusCodePages middleware with a default response handler that checks for responses with status codes
        /// between 400 and 599 that do not have a body.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStatusCodePages(this IApplicationBuilder app, int statusCode)
        {
            return UseStatusCodePages(app, new StatusCodePagesOptions(), statusCode);
	    }

        /// <summary>
        /// Adds a StatusCodePages middleware with the specified handler that checks for responses with status codes
        /// between 400 and 599 that do not have a body.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStatusCodePages(this IApplicationBuilder app, 
            Func<StatusCodeContext, Task> handler, int statusCode)
        {
                return UseStatusCodePages(app, new StatusCodePagesOptions() { HandleAsync = handler }, statusCode);
	    }

        /// <summary>
        /// Adds a StatusCodePages middleware to the pipeine. Specifies that responses should be handled by redirecting
        /// with the given location URL template. This may include a '{0}' placeholder for the status code. URLs starting
        /// with '~' will have PathBase prepended, where any other URL will be used as is.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="locationFormat"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStatusCodePagesWithRedirects(this IApplicationBuilder app, string locationFormat,
            int statusCode)
        {
            if (locationFormat.StartsWith("~"))
            {
                locationFormat = locationFormat.Substring(1);
                return UseStatusCodePages(app, context =>
                {
                    var location = string.Format(CultureInfo.InvariantCulture, locationFormat, context.HttpContext.Response.StatusCode);
                    context.HttpContext.Response.Redirect(context.HttpContext.Request.PathBase + location);
                    return Task.FromResult(0);
                }, statusCode);
            }
            else
            {
                return UseStatusCodePages(app, context =>
                {
                    var location = string.Format(CultureInfo.InvariantCulture, locationFormat, context.HttpContext.Response.StatusCode);
                    context.HttpContext.Response.Redirect(location);
                    return Task.FromResult(0);
                }, statusCode);
            }
        }
    }
}