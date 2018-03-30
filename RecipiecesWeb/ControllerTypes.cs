using System;
using System.Collections.Generic;
using System.Reflection;

namespace RecipiecesWeb
{
    public class ControllerTypes
    {
        public virtual IReadOnlyList<TypeInfo> Types => new List<TypeInfo>()
        {
            typeof(Sprocket).GetTypeInfo(),
            typeof(Widget).GetTypeInfo()
        };

        public class Sprocket { }
        public class Widget { }
    }
}
