using System.Text.Json.Serialization;

namespace HelloWorldService.Models
{
    public class Phone
    {
        [JsonPropertyName("number")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] 
        public string? Number { get; set; }

        [JsonPropertyName("phone_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public PhoneType PhoneType { get; set; }
    }
}
