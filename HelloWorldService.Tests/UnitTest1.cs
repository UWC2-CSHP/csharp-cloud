using System.Text.Json;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldService.Tests
{
    public class ContactTests
    {
        HttpClient client;

        // Called before every Test
        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5170/api/");
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
    }
}
