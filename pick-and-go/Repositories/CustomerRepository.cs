using PickAndGo.Models;
using PickAndGo.Data;


namespace PickAndGo.Repositories
{

    public class CustomerRepository
    {

        private readonly PickAndGoContext _db;

        public CustomerRepository(PickAndGoContext context)
        {
            _db = context;
        }

        //Note - Since we're only having 1 hard coded admin in the data base, new customers added will have property AdminUser set to N for NO.
        public void CreateRecord(string email, string firstName, string lastName, string phoneNumber)
        {
            Customer newCustomer = new Customer();
            newCustomer.EmailAddress = email;
            newCustomer.FirstName = firstName;
            newCustomer.LastName = lastName;
            newCustomer.PhoneNumber = phoneNumber;
            newCustomer.AdminUser = "N";

            _db.Add(newCustomer);
            _db.SaveChanges();
        }
    }
}
