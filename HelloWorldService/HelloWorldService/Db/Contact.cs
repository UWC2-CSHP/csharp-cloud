using System;
using System.Collections.Generic;

namespace HelloWorldService.Db
{
    public partial class Contact
    {
        public Contact()
        {
            ContactPhones = new HashSet<ContactPhone>();
        }

        public int ContactId { get; set; }
        public string ContactName { get; set; } = null!;
        public string? ContactEmail { get; set; }
        public int ContactAge { get; set; }
        public string? ContactNotes { get; set; }
        public DateTime ContactCreatedDate { get; set; }

        public virtual ICollection<ContactPhone> ContactPhones { get; set; }
    }
}
