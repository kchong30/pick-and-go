using PickAndGo.Models;
using PickAndGo.Data;
using System.Linq;
using PickAndGo.ViewModels;

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
        public Tuple<string, int> CreateRecord(string email, string firstName, string lastName, string phoneNumber)
        {
            Customer newCustomer = new Customer();
            newCustomer.EmailAddress = email;
            newCustomer.FirstName = firstName;
            newCustomer.LastName = lastName;
            newCustomer.PhoneNumber = phoneNumber;
            newCustomer.AdminUser = "N";
            newCustomer.DateSignedUp = DateTime.Now;

            string message = "";
            try
            {
                _db.Add(newCustomer);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return new Tuple<string, int>(message, newCustomer.CustomerId);
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

        public Customer ReturnCustomerByEmail(string email)
        {
            var customer = _db.Customers.Where(c => c.EmailAddress == email).FirstOrDefault();
            return customer;
        }

        public Customer GetCustomerRecord(int customerId)
        {
            var customer = _db.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault();

            return customer;
        }

        public string UpdateCustomerRecord(int customerId)
        {
            string editMessage = "";
            Customer customer = GetCustomerRecord(customerId);
            customer.DateLastOrdered = DateTime.Now;

            try
            {
                _db.Customers.Update(customer);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                editMessage = ex.Message;
            }

            return editMessage;
        }

    }
}
