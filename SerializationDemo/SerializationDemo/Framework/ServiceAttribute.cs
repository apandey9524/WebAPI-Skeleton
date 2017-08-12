using System;
using System.Linq;
using System.Reflection;

namespace SerializationDemo
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute(string name )
        {
            Name = name;
        }

        public string Name { get; private set; }

        public Service GetServiceDefinition(Type type)
        {
            var operations = type.GetMethods()
                       .Where(m => m.IsDefined(typeof(OperationAttribute), true))
                       .Select(m => m.GetCustomAttribute<OperationAttribute>().GetOperationDefinition(m))
                       .ToArray();
            var service = new Service { Name = Name  };
            service.Operations.AddRange(operations);
            return service;
        }
    }

}
