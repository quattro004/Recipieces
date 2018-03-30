using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Logging;

namespace Recipieces.PluginProvider
{
    public class Plugins
    {
        private readonly ILogger _logger;

        public Plugins(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<AssemblyPart> Discover(string pluginsPath)
        {
            _logger.LogDebug("Enumerating *Plugin.dll files in {0}", pluginsPath);
            var files = Directory.EnumerateFiles(pluginsPath, "*Plugin.dll");
            var assemblyParts = new List<AssemblyPart>();

            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                var assemblyName = assembly.GetName().Name;
                var assemblyTypes = assembly.GetTypes();

                foreach (var assemblyType in assemblyTypes)
                {
                    // Only need to look at classes since we are looking for an implementation of the
                    // IPlugin interface.
                    //
                    if(assemblyType.IsClass)
                    {
                        _logger.LogDebug("Looking for interfaces.Contains(typeof(IPlugin)) in the {0} type",
                            assemblyType.Name);
                        var interfaces = new List<Type>(assemblyType.GetInterfaces());
                        
                        if (interfaces.Contains(typeof(IPlugin)))
                        {
                            _logger.LogDebug("The {0} assembly contains at least one plugin, adding it to the list.",
                                assemblyName);
                            assemblyParts.Add(new AssemblyPart(assembly));
                            break;
                        }
                    }
                }
            }

            return assemblyParts;
        }
    }
}