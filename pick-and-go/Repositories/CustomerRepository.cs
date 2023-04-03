using PickAndGo.Models;
using PickAndGo.Data;
using System.Linq;
using PickAndGo.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace PickAndGo.Repositories
{

    public class CustomerRepository
    {

        private readonly PickAndGoContext _db;

        public CustomerRepository(PickAndGoContext context)
        {
            _db = context;
        }

        public Tuple<string, int> CreateRecord(string email, string firstName, string lastName, string phoneNumber)
        {
            Customer newCustomer = new Customer();
            newCustomer.EmailAddress = email;
            newCustomer.FirstName = firstName;
            newCustomer.LastName = lastName != null ? lastName : "";
            newCustomer.PhoneNumber = phoneNumber;
            newCustomer.AdminUser = "N";
            newCustomer.DateSignedUp = phoneNumber != "" ? DateTime.Now : null;

            string message = "";
            try
            {
                _ = _db.Add(newCustomer);
                _ = _db.SaveChanges();
            }
            catch (Exception ex)
            {
                message = "An error occurred while adding the customer to the database." +
                          " Please try again later." + " " + ex.Message;
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
                         DateSignedUp = c.DateSignedUp,
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
            vm.SignedUp = vm.DateSignedUp?.ToString("MM/dd/yyyy");
            vm.LastOrdered = vm.DateLastOrdered?.ToString("MM/dd/yyyy");

            return vm;
        }

        public Customer ReturnCustomerByEmail(string email)
        {
            var customer = _db.Customers.Where(c => c.EmailAddress == email).FirstOrDefault();
            return customer;
        }

        public string EditCustomer(int id, EditCustomerVM customerVM)
        {
            string editMessage = "";

            CustomerRepository cR = new CustomerRepository(_db);

            var vm = cR.ReturnCustomerById(id);

            try
            {
                _ = _db.Update(new Customer
                {
                    CustomerId = vm.CustomerId,
                    LastName = customerVM.LastName,
                    FirstName = customerVM.FirstName,
                    EmailAddress = vm.EmailAddress,
                    PhoneNumber = customerVM.PhoneNumber,
                    DateLastOrdered = vm.DateLastOrdered,
                    DateSignedUp = vm.DateSignedUp,
                    AdminUser = "N"
                });
                _ = _db.SaveChanges();
            }
            catch (Exception e)
            {
                editMessage = "An error occurred while updating the customer in the database." +
                              " Please try again later." + " " + e.Message;
            }

            if (editMessage == "")
            {
                editMessage = "Your profile has been updated";
            }
            return editMessage;
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
                _ = _db.Customers.Update(customer);
                _ = _db.SaveChanges();
            }
            catch (Exception ex)
            {
                editMessage = "An error occurred while updating the customer in the database." +
                              " Please try again later." + " " + ex.Message;
            }

            return editMessage;
        }

        public string UpdateCustomerSignUpDate(int customerId)
        {
            string editMessage = "";
            Customer customer = GetCustomerRecord(customerId);
            customer.DateSignedUp = DateTime.Now;

            try
            {
                _ = _db.Customers.Update(customer);
                _ = _db.SaveChanges();
            }
            catch (Exception ex)
            {
                editMessage = "An error occurred while updating the customer in the database." +
                              " Please try again later." + " " + ex.Message;
            }

            return editMessage;
        }

    }
}
