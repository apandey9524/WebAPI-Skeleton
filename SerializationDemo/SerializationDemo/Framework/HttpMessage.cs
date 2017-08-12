using System;
using System.IO;
using System.Text;

namespace SerializationDemo
{
    public class HttpRequest : IMessage
    {
        public HttpRequest(Uri url, string contents)
        {
            Url = url;
            var payload = Encoding.UTF8.GetBytes(contents);
            Contents = new MemoryStream(payload);
        }

        public Uri Url { get; private set; }

        public Stream Contents { get; private set; }
    }

    public class HttpResponse : IMessage
    {
        public HttpResponse()
        {
            Contents = new MemoryStream();
        }

        public Stream Contents { get; private set; }

        public string GetText()
        {
            return Encoding.UTF8.GetString((Contents as MemoryStream).ToArray());
        }
    }

}
