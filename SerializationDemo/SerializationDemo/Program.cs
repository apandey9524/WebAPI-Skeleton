using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SerializationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Run synchronously.
            // RunAsync().GetAwaiter().GetResult();
            Console.WriteLine(ConcatAsync("Hello ", "World!").Result);
        }

        private static async Task<string> ConcatAsync(string prefix, string suffix)
        {
            var rq = new
            {
                itemA = new { value = prefix },
                itemB = new { value = suffix }
            };
            var json = JObject.FromObject(rq).ToString();
            var rqMsg = new HttpRequest(new Uri("http://localhost/stringService/concat"), json);
            var invoker = new ApiInvoker();
            var rs = (await invoker.ExecuteAsync(rqMsg)) as HttpResponse;
            var response = rs.GetText();
            var jsonRs = JObject.Parse(response);
            return jsonRs["Value"].ToString();
        }


        private static async Task RunAsync()
        {
            var person = new Person
            {
                Name = "John Doe",
                Email = "john.doe@email.com",
                Age = 25,
                Id = "100"
            };
            await Serializer.Psv.SerializeAsync(person, "person.psv");
            var clone = await Serializer.Psv.DeSerializeAsync<Person>("person.psv");
            await Serializer.Xml.SerializeAsync(person, "person.xml");
            clone = await Serializer.Xml.DeSerializeAsync<Person>("person.xml");
            await Serializer.Json.SerializeAsync(person, "person.json");
            clone = await Serializer.Json.DeSerializeAsync<Person>("person.json");
        }
    }
}
