using System.Text.Json;
using System.Text.Json.Serialization;

var client = new HttpClient();

client.BaseAddress = new Uri("http://localhost:5170/api/");
// client.DefaultRequestHeaders.Add("Accept", "application/json");

// Post a new contact
var newContact = new Contact
{
    Name = "New Name",
    Phones = new[] {
            new Phone {
                Number = "425-111-2222",
                PhoneType = PhoneType.Mobile
            }
        }
};

var newJson = JsonSerializer.Serialize(newContact);

var postContent = new StringContent(
        newJson,
        System.Text.Encoding.UTF8,
        "application/json");

var postResult = await client.PostAsync("contacts", postContent);

Console.WriteLine(postResult.StatusCode);

// Get All Contacts
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
    [JsonPropertyName("number")]
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