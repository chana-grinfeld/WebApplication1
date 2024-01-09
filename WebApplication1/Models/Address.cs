using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Address
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
