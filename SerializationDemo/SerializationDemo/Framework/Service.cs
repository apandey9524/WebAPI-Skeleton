using System;
using System.Collections.Generic;

namespace SerializationDemo
{
    public class Service
    {
        private Type _serviceType = null;

        public string Name { get; set; }

        public List<Operation> Operations { get; } = new List<Operation>();

        public bool Match(IMessage rq)
        {
            var msg = rq as HttpRequest;
            return msg?.Url.Segments[1].Equals(Name + "/") == true;
        }

        public Operation GetMatchingOperation(IMessage req)
        {
            return Operations.Find(op => op.Match(req) == true);
        }
    }

}
