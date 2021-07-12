using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Spotters.Core.Handlers
{
    public class ObjectHandler
    {
        public static bool IsPropertyExist(dynamic settings, string name)
        {
            if (settings is ExpandoObject)
                return ((IDictionary<string, object>)settings).ContainsKey(name);

            return settings.GetType().GetProperty(name) != null;
        }
    }
}
