using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using RecipiecesWeb.Controllers;

namespace RecipiecesWeb
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public virtual void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        { 
            // This is designed to run after the default ControllerTypeProvider, 
            // so the list of 'real' controllers has already been populated.
            foreach (var entityType in new ControllerTypes().Types)
            {
                var typeName = entityType.Name + "Controller";
                if (!feature.Controllers.Any(t => t.Name == typeName))
                {
                    // There's no 'real' controller for this entity, so add the generic version.
                    var controllerType = typeof(GenericController<>)
                        .MakeGenericType(entityType.AsType()).GetTypeInfo();
                    feature.Controllers.Add(controllerType);
                }
            }
        }
    }
}