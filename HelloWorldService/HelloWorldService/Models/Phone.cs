using System.Text.Json.Serialization;

namespace HelloWorldService.Models
{
    public class Phone
    {
        [JsonPropertyName("number")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] public string? Number { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PhoneType PhoneType { get; set; }
    }
}
