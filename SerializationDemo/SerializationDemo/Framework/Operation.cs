using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SerializationDemo
{
    public class Operation
    {

        public Operation(MethodInfo method)
        {
            _method = method;
            _serviceType = method.DeclaringType;
        }

        private Type _serviceType = null;

        private MethodInfo _method = null;

        public string Name { get; set; }

        public Type ReturnType { get; set; }

        public Parameter[] InputParamters { get; set; }

        public bool Match(IMessage rq)
        {
            var msg = rq as HttpRequest;
            return msg?.Url.Segments[2].Equals(Name) == true;
        }

        public async Task<IMessage> ExecuteAsync(IMessage msg)
        {
            // convert msg t0 map of values.
            var rqContents = await ReadRequestAsync(msg);
            // Get values that need to be passed to the function from the map.
            var args = _method.GetParameters();
            var values = args.Select(x => rqContents[x.Name]).ToArray();
            // Invoke the function
            var service = Activator.CreateInstance(_serviceType);
            var output = _method.Invoke(service, values);
            // Convert result into a response message.
            var response = await GetResponseAsync(output);
            return response;
        }

        private async Task<IMessage> GetResponseAsync(object output)
        {
            var formatter = new JsonFormatter();
            var response = new HttpResponse();
            await formatter.WriteAsync(output, response.Contents);
            return response;
        }

        private async Task<GenericObject> ReadRequestAsync(IMessage msg)
        {
            var formatter = new JsonFormatter();
            formatter.Serializer.Converters.Add(new GenericObjectConverter(InputParamters));
            return await formatter.ReadAsync<GenericObject>(msg.Contents);
        }
    }

}
