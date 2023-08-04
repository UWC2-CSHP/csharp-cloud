using System.Text.Json.Serialization;

namespace HelloWorldService.Tests
{
    public class Phone
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("phone_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PhoneType PhoneType { get; set; }
    }
}
