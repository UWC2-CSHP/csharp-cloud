using System.Text.Json;
using System.Text.Json.Serialization;

var client = new HttpClient();

client.BaseAddress = new Uri("http://localhost:5170/api/");
// client.DefaultRequestHeaders.Add("Accept", "application/json");

var result = await client.GetAsync("contacts");

var json = await result.Content.ReadAsStringAsync();

Console.WriteLine(json);

var list = JsonSerializer.Deserialize<List<Contact>>(json);

Console.ReadLine();

public class Contact
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("date_added")]
    public DateTime DateAdded { get; set; }

    [JsonPropertyName("phones")]
    public Phone[] Phones { get; set; }
}

public class Phone
{
    [JsonPropertyName("phone_number")]
    public string Number { get; set; }

    [JsonPropertyName("phone_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PhoneType PhoneType { get; set; }
}

public enum PhoneType
{
    Nil,
    Home,
    Mobile,
}