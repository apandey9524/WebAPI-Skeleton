using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace SerializationDemo
{
    public class JsonFormatter : IFormatter
    {
        public JsonSerializer Serializer { get; } = new JsonSerializer()
        {
            Formatting = Formatting.Indented
        };

        public Task<T> ReadAsync<T>(Stream stream)
        {
            using (var txtReader = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(txtReader))
                {
                    var value = Serializer.Deserialize<T>(jsonReader);
                    return Task.FromResult(value);
                }
            }
        }

        public Task WriteAsync<T>(T obj, Stream stream)
        {
            using (var txtWriter = new StreamWriter(stream))
            {
                using (var jsonWriter= new JsonTextWriter(txtWriter))
                {
                    Serializer.Serialize(jsonWriter, obj, typeof(T));
                    return Task.CompletedTask;
                }
            }
        }
    }
}
