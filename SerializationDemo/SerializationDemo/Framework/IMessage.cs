using System.IO;

namespace SerializationDemo
{
    public interface IMessage
    {
        Stream Contents { get; }
    }

}
