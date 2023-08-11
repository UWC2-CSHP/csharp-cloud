using System.Text.Json.Serialization;

namespace HelloWorldService.Models
{
    /// <summary>
    /// This is PhoneType documentation
    /// Nil is not used
    /// Home is for landlines
    /// Mobile is for cellular
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PhoneType
    {
        /// <summary>
        /// This is the Nil Value
        /// </summary>
        Nil, // Important to have a zero value
        Home,
        Mobile,
    }
}
