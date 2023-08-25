using HelloWorldService.Models;
using System.Collections.Generic;

namespace HelloWorldService
{
    public interface IContactRepository
    {
        IEnumerable<Contact> Contacts { get; }
    }

    public class ContactRepository : IContactRepository
    {
        private static int currentId = 101;

        private static List<Contact> contacts = new List<Contact>();

        public ContactRepository()
        {
            contacts.Add(new Contact { Name = "First One" });
            contacts.Add(new Contact { Name = "Second One" });
            contacts.Add(new Contact { Name = "Third One" });
            contacts.Add(new Contact { Name = "Fourth One" });
        }

        public IEnumerable<Contact> Contacts
        {
            get
            {
                return contacts;
            }
        }
    }
}