using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PickAndGo.ViewModels
{
    [Keyless]
    public class EditCustomerVM 
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
