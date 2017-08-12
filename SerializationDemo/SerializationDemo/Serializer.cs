using System.IO;
using System.Threading.Tasks;

namespace SerializationDemo
{
    public class Serializer
    {
        private static readonly string OutputDirectory = @"C:\Users\apandey\Documents\visual studio 2017\Projects\SerializationDemo\SerializationDemo\Output\";
        public IFormatter Formatter { get; set; }

        public static Serializer Psv = new Serializer(new PsvFormatter());
        public static Serializer Xml = new Serializer(new XmlFormatter());
        public static Serializer Json = new Serializer(new JsonFormatter());

        public Serializer(IFormatter formatter)
        {
            Formatter = formatter;
        }

        public async Task SerializeAsync<T>(T obj, string path)
        {
            path = Path.Combine(OutputDirectory, path);
            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                await Formatter.WriteAsync(obj, file);
            }
        }

        public async Task<T> DeSerializeAsync<T>(string path)
        {
            path = Path.Combine(OutputDirectory, path);
            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return await Formatter.ReadAsync<T>(file);
            }
        }
    }
}
