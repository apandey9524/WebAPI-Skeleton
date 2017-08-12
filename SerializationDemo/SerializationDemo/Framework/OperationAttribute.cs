using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SerializationDemo
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OperationAttribute : Attribute
    {
        public OperationAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public Operation GetOperationDefinition(MethodInfo method)
        {
            List<Parameter> parameters = new List<Parameter>();
            var args = method.GetParameters();
            foreach( var arg in args )
            {
                var attr = arg.GetCustomAttributes<OperationArgAttribute>().SingleOrDefault();
                var parameter = attr == null ?
                    new Parameter { Name = arg.Name, ParameterName = arg.Name, Type = arg.ParameterType } :
                    attr.GetParameterDefinition(arg);
                parameters.Add(parameter);
            }
            return new Operation(method)
            {
                Name = Name,
                ReturnType = method.ReturnType,
                InputParamters = parameters.ToArray()
            };
        }
    }

}
