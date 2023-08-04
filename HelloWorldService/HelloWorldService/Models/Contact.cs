using System;
using System.Text.Json.Serialization;

namespace HelloWorldService.Models
{
    public class Contact
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("date_added")]
        public DateTime DateAdded { get; set; }

        [JsonPropertyName("phones")]
        public Phone[]? Phones { get; set; }
    }
}
