using Microsoft.AspNetCore.Mvc;
using PickAndGo.Data;
using PickAndGo.Models;
using PickAndGo.Repositories;
using System.Diagnostics;

namespace PickAndGo.Controllers
{
    public class AdminController : Controller
    {
        private readonly PickAndGoContext _db;

        public AdminController(PickAndGoContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerList()
        {
            CustomerRepository cR = new CustomerRepository(_db);
            var vm = cR.ReturnAllCustomers();

            return View(vm);
        }
        public IActionResult CustomerDetail(int customerId)
        {
            CustomerRepository cR = new CustomerRepository(_db);
            var vm = cR.ReturnCustomerById(customerId);

            return View(vm);
        }

    }
}