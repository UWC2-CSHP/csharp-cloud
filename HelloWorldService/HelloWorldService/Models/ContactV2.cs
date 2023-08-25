using System;
using System.Text.Json.Serialization;

namespace HelloWorldService.Models
{
    /// <summary>
    /// This is the Contact data object
    /// </summary>
    public class ContactV2
    {
        /// <summary>
        /// This is the primary key
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("date_added")]
        public DateTime DateAdded { get; set; }

        [JsonPropertyName("phones")]
        public Phone[]? Phones { get; set; }
    }
}
