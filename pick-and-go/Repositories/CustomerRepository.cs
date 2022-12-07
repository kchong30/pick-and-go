using PickAndGo.Models;
using PickAndGo.Data;
using pick_and_go.ViewModel;
using System.Linq;

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

        public IEnumerable<CustomerVM> ReturnAllCustomers()
        {
            var vm = from c in _db.Customers
                     let oCount = (from o in _db.OrderHeaders where c.CustomerId == o.CustomerId select o).Count()
                     select new CustomerVM
                     {
                         CustomerId = c.CustomerId,
                         EmailAddress = c.EmailAddress,
                         DateLastOrdered = c.DateLastOrdered,
                         FirstName = c.FirstName,
                         LastName = c.LastName,
                         PhoneNumber = c.PhoneNumber,
                         NumbersOfOrders = oCount
                     };

            return vm;
        }

        public CustomerVM ReturnCustomerById(int customerId)
        {
            var vm = ReturnAllCustomers().Where(c => c.CustomerId == customerId).FirstOrDefault();

            return vm;
        }

    }
}
