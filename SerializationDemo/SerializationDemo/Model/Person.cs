using Newtonsoft.Json;
using System;

namespace SerializationDemo
{
    [Serializable]
    public class Person
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }
}
