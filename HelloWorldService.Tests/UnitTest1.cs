using System.Text.Json;
using FluentAssertions;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace HelloWorldService.Tests
{
    public class Token
    {
        [JsonPropertyName("token")]
        public string TokenString { get; set; }
    }
    public class ContactTests
    {
        HttpClient client;

        // Called before every Test
        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5170/api/");

            var tokenResult = client.GetAsync("token?userName=dave&password=pass").Result;
            var tokenJson = tokenResult.Content.ReadAsStringAsync().Result;
            var token = JsonSerializer.Deserialize<Token>(tokenJson);

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.TokenString);
        }

        [Test]
        public async Task AddNewContact()
        {
            // Arrange
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

            var postContent = new StringContent(newJson,
                Encoding.UTF8, "application/json");

            // Act
            var postResult = await client.PostAsync("contacts", postContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, postResult.StatusCode);

            postResult.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public async Task TestGetAll()
        {
            var result = await client.GetAsync("contacts");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task TestDelete_ValidContactId()
        {
            var postResult = await CreateContact("TestDelete_ValidContactId");

            var json = await postResult.Content.ReadAsStringAsync();

            var contact = JsonSerializer.Deserialize<Contact>(json);

            var result = await client.DeleteAsync("contacts/" + contact.Id);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task TestDelete_InvalidContactStringId()
        {
            var result = await client.DeleteAsync("contacts/abc");

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task TestDelete_InvalidContactId()
        {
            var result = await client.DeleteAsync("contacts/1");

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task TestGetSpecific_Good()
        {
            var postResult = await CreateContact("TestGetSpecific_Good");

            var json = await postResult.Content.ReadAsStringAsync();

            var contact = JsonSerializer.Deserialize<Contact>(json);

            var result = await client.GetAsync("contacts/" + contact.Id);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task TestGetSpecific_Bad()
        {
            var result = await client.GetAsync("contacts/10211");

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task TestAddNewContact()
        {
            var postResult = await CreateContact("AddNewContactTest");

            Assert.AreEqual(HttpStatusCode.Created, postResult.StatusCode);

            postResult.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public async Task TestAddNewContact_NameNull()
        {
            var postResult = await CreateContact(null);

            postResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task TestAddNewContact_NameEmpty()
        {
            var postResult = await CreateContact("");

            postResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private async Task<HttpResponseMessage> CreateContact(string name)
        {
            var newContact = new Contact
            {
                Name = name,
                Phones = new[] {
                    new Phone {
                        Number = "425-111-2222",
                        PhoneType = PhoneType.Mobile
                    }
                }
            };
            var newJson = JsonSerializer.Serialize(newContact);
            var postContent = new StringContent(newJson, Encoding.UTF8, "application/json");
            var postResult = await client.PostAsync("contacts", postContent);

            return postResult;
        }


    }
}
