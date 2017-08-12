using System;
using System.Collections.Generic;

namespace SerializationDemo
{
    public class GenericObject
    {
        public Dictionary<string, object> Contents = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public object this[string name]
        {
            get
            {
                object value;
                if (Contents.TryGetValue(name, out value) == true)
                    return value;
                else
                    return null;
            }
            set
            {
                Contents[name] = value;
            }
        }
    }

}
