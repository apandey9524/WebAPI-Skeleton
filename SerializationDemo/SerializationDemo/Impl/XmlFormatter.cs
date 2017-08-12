using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializationDemo
{
    public class XmlFormatter : IFormatter
    {
        public Task<T> ReadAsync<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            var value = (T)serializer.Deserialize(stream);
            return Task.FromResult(value);
        }

        public Task WriteAsync<T>(T obj, Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, obj);
            return Task.CompletedTask;
        }
    }
}
