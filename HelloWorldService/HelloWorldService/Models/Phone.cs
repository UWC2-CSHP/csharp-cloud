using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace HelloWorldService.Models
{
    public class Phone
    {
        [JsonPropertyName("number")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DisplayName("number")]
        [Required]
        public string? Number { get; set; }

        [JsonPropertyName("phone_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DisplayName("phone_type")]
        [Required]
        public PhoneType? PhoneType { get; set; }
    }
}
