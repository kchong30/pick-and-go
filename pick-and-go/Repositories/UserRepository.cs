using Microsoft.EntityFrameworkCore;
using PickAndGo.Data;
using PickAndGo.Models;
using PickAndGo.ViewModels;

namespace PickAndGo.Repositories
{
    public class UserRepository
    {
        ApplicationDbContext _aspContext;
        PickAndGoContext _pgContext;
        

        public UserRepository(ApplicationDbContext aspContext, PickAndGoContext pgContext)
        {
            this._aspContext = aspContext;
            this._pgContext = pgContext;
        }

        public IEnumerable<UserVM> GetAllUsers()
        {
            IEnumerable<UserVM> users = _aspContext.Users.Select(u => new UserVM() { Email = u.Email });
            return users;
        }

        public string GetFullName(string email)
        {
            var firstName = _pgContext.Customers.Where(r => r.EmailAddress == email).FirstOrDefault().FirstName;
            var lastName = _pgContext.Customers.Where(r => r.EmailAddress == email).FirstOrDefault().LastName;
            var fullName = firstName + " " + lastName;
            return fullName;
        }

    }
}
