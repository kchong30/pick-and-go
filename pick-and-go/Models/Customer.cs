using System;
using System.Collections.Generic;

namespace pick_and_go.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Favorites = new HashSet<Favorite>();
            OrderHeaders = new HashSet<OrderHeader>();
            Dietaries = new HashSet<DietaryType>();
        }

        public int CustomerId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AdminUser { get; set; }
        public DateTime? DateSignedUp { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }

        public virtual ICollection<DietaryType> Dietaries { get; set; }
    }
}
