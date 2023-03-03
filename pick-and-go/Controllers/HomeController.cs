using Microsoft.AspNetCore.Mvc;
using PickAndGo.Data;
using PickAndGo.Models;
using PickAndGo.Repositories;
using System.Diagnostics;

namespace PickAndGo.Controllers
{
    public class HomeController : Controller
    {

        private readonly PickAndGoContext _db;

        public HomeController(PickAndGoContext context)
        {
            _db = context;
        }


        public IActionResult Index()
        {
            CustomerRepository cr = new CustomerRepository(_db);
            var customer = cr.ReturnCustomerByEmail(User.Identity.Name);
            var customerId = customer.CustomerId.ToString();
            HttpContext.Session.SetString("customerid", customerId);
            return View();
        }


    }
}