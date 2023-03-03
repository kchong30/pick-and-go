using Microsoft.EntityFrameworkCore;
using PickAndGo.Models;

namespace PickAndGo.ViewModels
{
    [Keyless]
    public class CustomerVM
    {
        public int CustomerId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string AdminUser { get; set; } = null!;
        public DateTime? DateSignedUp { get; set; }
        public DateTime? DateLastOrdered { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }

        public int? NumbersOfOrders { get; set; }

    }
}
