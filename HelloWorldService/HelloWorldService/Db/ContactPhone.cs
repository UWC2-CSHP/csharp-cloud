using System;
using System.Collections.Generic;

namespace HelloWorldService.Db
{
    public partial class ContactPhone
    {
        public int ContactPhoneId { get; set; }
        public int ContactId { get; set; }
        public string ContactPhoneNumber { get; set; } = null!;
        public int ContactPhoneType { get; set; }

        public virtual Contact Contact { get; set; } = null!;
    }
}
