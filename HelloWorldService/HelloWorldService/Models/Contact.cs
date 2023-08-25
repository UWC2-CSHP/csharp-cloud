using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HelloWorldService.Models
{
    /// <summary>
    /// This is the Contact data object
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// This is the primary key
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        [Required]
        [DisplayName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("date_added")]
        public DateTime? DateAdded { get; set; }

        [JsonPropertyName("phones")]
        [Required]
        [DisplayName("phones")]
        public Phone[]? Phones { get; set; }
    }
}
