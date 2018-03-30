using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Recipieces.PluginProvider;

namespace RecipiecesWeb
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogDebug("Configuring services");
            // create an assembly part from a class's assembly
            // var assembly = typeof(GenericControllerFeatureProvider).GetTypeInfo().Assembly;
            // services.AddMvc()
            //     .AddApplicationPart(assembly);

            // Can load assemblies at runtime using assembly parts as plugins
            //
            _logger.LogDebug("Adding MVC");            
            services.AddMvc()
                .ConfigureApplicationPartManager(apm =>
                {
                    var pluginsPath = Configuration.GetValue<string>("PluginsPath");

                    _logger.LogDebug("Discovering plugins at {0}", pluginsPath);
                    var plugins = new Plugins(_logger).Discover(pluginsPath);

                    foreach (var plugin in plugins)
                    {
                        var pluginAssembly = plugin.Assembly;

                        _logger.LogDebug("Adding application parts and configuring views for the {0} plugin",
                            pluginAssembly.GetName().Name);

                        apm.ApplicationParts.Add(plugin);
                        //
                        // Add any views provided in the plugin.
                        //   
                        services.Configure<RazorViewEngineOptions>(options => 
                        { 
                            options.FileProviders.Add(new EmbeddedFileProvider(pluginAssembly));
                        });
                    }
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
