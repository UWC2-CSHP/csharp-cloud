using HelloWorldService.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace HelloWorldService
{
    public interface IContactRepository
    {
        IEnumerable<Contact> Contacts { get; }
        void Add(Contact contact);
        bool Update(int contactId, Contact contact);
        bool Delete(int contactId);
    }

    public class ContactRepository : IContactRepository
    {
        private IMemoryCache memoryCache;

        private static int currentId = 101;

        private static List<Contact> contacts = new List<Contact>();

        public ContactRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;

            //contacts.Add(new Contact { Name = "First One" });
            //contacts.Add(new Contact { Name = "Second One" });
            //contacts.Add(new Contact { Name = "Third One" });
            //contacts.Add(new Contact { Name = "Fourth One" });
        }

        public IEnumerable<Contact> Contacts
        {
            get
            {
                List<Contact> items;

                if (!memoryCache.TryGetValue("MyContacts", out items))
                {
                    // load data from database
                    items = contacts;

                    // Store the "database" results in the cache for 20 seconds
                    memoryCache.Set("MyContacts", items,
                        new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(System.TimeSpan.FromMinutes(60)));

                }

                // return data
                return items;
            }
        }

        public void Add(Contact contact)
        {
            contact.Id = currentId++;
            contact.DateAdded = DateTime.UtcNow;
            //value.AddedByWho = "tbd";

            contacts.Add(contact);
        }

        public bool Delete(int contactId)
        {
            var rowsDeleted = contacts.RemoveAll(t => t.Id == contactId);

            return (rowsDeleted > 0);
        }

        public bool Update(int contactId, Contact contact)
        {
            var contactFound = contacts.FirstOrDefault(t => t.Id == contactId);

            if (contactFound != null)
            {
                contactFound.Id = contactId;
                contactFound.Name = contact.Name;
                contactFound.Phones = contact.Phones;

                return true;
            }

            return false;
        }
    }
}