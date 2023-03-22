using System;
using System.Collections.Generic;

namespace PickAndGo.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Favorites = new HashSet<Favorite>();
            OrderHeaders = new HashSet<OrderHeader>();
        }

        public int CustomerId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string AdminUser { get; set; } = null!;
        public DateTime? DateSignedUp { get; set; }
        public DateTime? DateLastOrdered { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }

    }
}
