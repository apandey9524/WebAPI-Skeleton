using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationDemo
{
    public class PsvFormatter : IFormatter
    {
        public async Task<T> ReadAsync<T>(Stream stream)
        {
            var contents = string.Empty;
            using (var reader = new StreamReader(stream))
            {
                contents = await reader.ReadToEndAsync();
            }
            // Extract values from the file.
            var map = contents
                            .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(t => t.Split('='))
                            .ToDictionary(x => x[0], x => x[1], StringComparer.OrdinalIgnoreCase);
            var instance = Activator.CreateInstance<T>();
            typeof(T).GetProperties()
                    .ToList()
                    .ForEach(p =>
                   {
                       var attr = p.GetCustomAttributes(typeof(PsvFieldAttribute), true).SingleOrDefault() as PsvFieldAttribute;
                       var name = attr?.FieldName ?? p.Name;
                       var valueStr = map[name];
                       var value = Convert.ChangeType(valueStr, p.PropertyType);
                       p.SetValue(instance, value);
                   });
            return instance;
        }

        public async Task WriteAsync<T>(T obj, Stream stream)
        {
            var values = obj.GetType()
                            .GetProperties()
                            .Select(p =>
                            {
                                var attr = p.GetCustomAttributes(typeof(PsvFieldAttribute), true).SingleOrDefault() as PsvFieldAttribute;
                                var name = attr?.FieldName ?? p.Name;
                                return new { Field = name, Value = p.GetValue(obj) };
                            })
                            .ToArray();
            var buffer = new StringBuilder();
            Array.ForEach(values, x => buffer.Append(x.Field).Append("=").Append(x.Value).Append("|"));
            var bytes = Encoding.UTF8.GetBytes(buffer.ToString());
            await stream.WriteAsync(bytes, 0, bytes.Length);
            await stream.FlushAsync(); 
        }
    }
}
