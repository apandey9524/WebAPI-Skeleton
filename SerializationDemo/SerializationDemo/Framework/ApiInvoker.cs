using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

namespace SerializationDemo
{
    public class ApiInvoker
    {
        public ApiInvoker()
        {
            var services = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .Where(t => t.IsDefined(typeof(ServiceAttribute)))
                                   .Select(t => t.GetCustomAttribute<ServiceAttribute>().GetServiceDefinition(t));
            Services.AddRange(services);
        }

        public List<Service> Services { get; } = new List<Service>();

        public Task<IMessage> ExecuteAsync(IMessage req)
        {
            var service = Services.Find(s => s.Match(req));
            var op = service.GetMatchingOperation(req);
            return op.ExecuteAsync(req);
        }
    }

}
