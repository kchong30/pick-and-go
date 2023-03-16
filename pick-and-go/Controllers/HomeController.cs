using Microsoft.AspNetCore.Mvc;
using PickAndGo.Data;
using PickAndGo.Models;
using PickAndGo.Repositories;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Diagnostics;
using PickAndGo.Services;

namespace PickAndGo.Controllers
{
    public class HomeController : Controller
    {

        private readonly PickAndGoContext _db;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;



        public HomeController(PickAndGoContext context, IConfiguration configuration, IEmailService emailService)
        {
            _db = context;
            _configuration = configuration;
            _emailService = emailService;

        }


        public IActionResult Index()
        {


            CustomerRepository cr = new CustomerRepository(_db);
            if (User.Identity.IsAuthenticated && User.IsInRole("Customer")) { 
                var customer = cr.ReturnCustomerByEmail(User.Identity.Name);
                var customerId = customer.CustomerId.ToString();
                HttpContext.Session.SetString("customerid", customerId);
            }


            return View();
        }


        public IActionResult Landing()
        {
            CustomerRepository cr = new CustomerRepository(_db);
            if (User.Identity.IsAuthenticated)
            {
                var customer = cr.ReturnCustomerByEmail(User.Identity.Name);
                var customerId = customer.CustomerId.ToString();
                HttpContext.Session.SetString("customerid", customerId);
            }
            return View();
        }


    }
}