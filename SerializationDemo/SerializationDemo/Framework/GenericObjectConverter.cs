using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationDemo
{
    public class GenericObjectConverter : JsonConverter
    {
        public GenericObjectConverter(Parameter[] parameters )
        {
            Parameters = parameters;
        }

        public Parameter[] Parameters { get; set; }

        public override bool CanConvert(Type objectType)
        {
            return typeof(GenericObject) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            GenericObject obj = new GenericObject();
            var json = JObject.ReadFrom(reader) as JObject;
            foreach( var param in Parameters )
            {
                var property = json.Property(param.Name);
                var valueReader = property.Value.CreateReader();
                var value = serializer.Deserialize(valueReader, param.Type);
                obj[param.ParameterName] = value;
            }
            return obj;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
