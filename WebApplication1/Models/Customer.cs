using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Addresses = new HashSet<Address>();
            Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public string NameFull { get; set; } = null!;
        public Guid CustomerNumber { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
