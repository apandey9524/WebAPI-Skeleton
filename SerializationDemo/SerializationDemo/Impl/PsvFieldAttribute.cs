using System;

namespace SerializationDemo
{
    [AttributeUsage( AttributeTargets.Property)]
    public class PsvFieldAttribute : Attribute
    {
        public PsvFieldAttribute(string name)
        {
            FieldName = name;
        }

        public string FieldName { get; private set; }
    }
}
