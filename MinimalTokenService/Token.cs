using System.Text.Json.Serialization;

namespace MinimalTokenService
{
    public class TokenRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }

    public class Token
    {
        public int UserId { get; set; }
        public DateTime Expires { get; set; }
    }
}
