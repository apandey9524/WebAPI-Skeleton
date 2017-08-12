using System.IO;
using System.Threading.Tasks;

namespace SerializationDemo
{
    public interface IFormatter
    {
        Task WriteAsync<T>(T obj, Stream stream);

        Task<T> ReadAsync<T>(Stream stream);
    }
}
