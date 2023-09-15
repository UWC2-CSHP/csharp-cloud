using System.Text.Json.Serialization;

namespace TokenLibrary.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
