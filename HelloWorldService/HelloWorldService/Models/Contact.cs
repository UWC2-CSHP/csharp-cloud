using System;
using System.Text.Json.Serialization;

namespace HelloWorldService.Models
{
    public class Contact
    {
        [JsonPropertyName("ID")]
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime DateAdded { get; set; }

        public Phone[]? Phones { get; set; }
    }
}
