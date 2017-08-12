using System;
using System.Reflection;

namespace SerializationDemo
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class OperationArgAttribute : Attribute
    {
        public OperationArgAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }

        public Parameter GetParameterDefinition(ParameterInfo arg)
        {
            return new Parameter { Name = Name, ParameterName = arg.Name, Type = arg.ParameterType };
        }
    }

}
