using HelloWorldService.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore; // ADD ME

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
        private static Db.HelloWorldContext database = new Db.HelloWorldContext();

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
                    var contacts = database.Contacts
                        .Include(t => t.ContactPhones);

                    items = contacts
                        .Select(c => new Contact
                        {
                            Id = c.ContactId,
                            Name = c.ContactName,
                            DateAdded = c.ContactCreatedDate,
                            Phones = c.ContactPhones.Select(p => new Phone
                            {
                                Number = p.ContactPhoneNumber,
                                PhoneType = (PhoneType)p.ContactPhoneType,

                            }).ToArray(),
                        })
                        .ToList();

                    // Store the "database" results in the cache for 20 seconds
                    memoryCache.Set("MyContacts", items,
                        new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(System.TimeSpan.FromSeconds(1)));

                }

                // return data
                return items;
            }
        }

        public void Add(Contact contact)
        {
            try
            {
                var dbContact = new Db.Contact
                {
                    ContactName = contact.Name,
                    ContactPhones = contact.Phones.Select(p => new Db.ContactPhone
                    {
                        ContactPhoneNumber = p.Number,
                        ContactPhoneType = (int)p.PhoneType,
                    }).ToArray(),
                };

                database.Contacts.Add(dbContact);

                database.SaveChanges();

                contact.Id = dbContact.ContactId;
                contact.Name = dbContact.ContactName;
                contact.DateAdded = dbContact.ContactCreatedDate;
                contact.Phones = dbContact.ContactPhones
                    .Select(p => new Phone
                    {
                        Number = p.ContactPhoneNumber,
                        PhoneType = (PhoneType)p.ContactPhoneType,
                    }).ToArray();

            }
            catch (DbUpdateException ex)
            {
                database.Dispose();
                database = new Db.HelloWorldContext();
                throw;
                //throw new DatabaseException("Missing PhoneNumber")
            }

        }

        public bool Delete(int contactId)
        {
            var contact = database.Contacts.Include(t => t.ContactPhones).FirstOrDefault(t => t.ContactId == contactId);

            if (contact == null)
            {
                return false;
            }
            database.ContactPhones.RemoveRange(contact.ContactPhones);
            database.Contacts.Remove(contact);

            database.SaveChanges();

            return true;
        }

        public bool Update(int contactId, Contact updatedContact)
        {
            var dbContact = database.Contacts.Include(t => t.ContactPhones).FirstOrDefault(t => t.ContactId == contactId);

            if (dbContact == null)
            {
                return false;
            }

            dbContact.ContactName = updatedContact.Name;

            if (updatedContact.Phones != null)
            {
                dbContact.ContactPhones = updatedContact.Phones.Select(p => new Db.ContactPhone
                {
                    ContactId = contactId,
                    ContactPhoneNumber = p.Number,
                    ContactPhoneType = (int)p.PhoneType,
                }).ToArray();
            }

            database.Contacts.Update(dbContact);

            // BUGBUG:'The association between entity types 'Contact' and 'ContactPhone' has been severed, but the relationship is either marked as required or is implicitly required because the foreign key is not nullable. If the dependent/child entity should be deleted when a required relationship is severed
            database.SaveChanges();

            updatedContact.Id = dbContact.ContactId;
            updatedContact.Name = dbContact.ContactName;
            updatedContact.DateAdded = dbContact.ContactCreatedDate;
            updatedContact.Phones = dbContact.ContactPhones
                .Select(p => new Phone
                {
                    Number = p.ContactPhoneNumber,
                    PhoneType = (PhoneType)p.ContactPhoneType,
                }).ToArray();

            // Automapper version
            //updatedContact = mapper.Map<Contact>(contact);

            return true;

        }
    }
}