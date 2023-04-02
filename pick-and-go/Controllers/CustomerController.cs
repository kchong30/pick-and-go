using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PickAndGo.Data;
using PickAndGo.Models;
using PickAndGo.Repositories;
using PickAndGo.ViewModels;
using System.Diagnostics;

namespace PickAndGo.Controllers
{
    public class CustomerController : Controller
    {
        private readonly PickAndGoContext _db;

        public CustomerController(PickAndGoContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public IActionResult CustomerDetails(string message)
        {
            CustomerRepository cR = new CustomerRepository(_db);

            int customerId = Convert.ToInt32(HttpContext.Session.GetString("customerid"));
            var vm = cR.ReturnCustomerById(customerId);

            ViewData["Message"] = message;
            return View(vm);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerEdit(int id)
        {
            CustomerRepository cR = new CustomerRepository(_db);
            var vm = cR.ReturnCustomerById(id);
            if (vm == null)
            {
                return View("Error");
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerEdit(int id, EditCustomerVM customerVM)
        {
            string editMessage = "";

            CustomerRepository cR = new CustomerRepository(_db);
            if (ModelState.IsValid)
            {
                editMessage = cR.EditCustomer(id, customerVM);
            }
            return RedirectToAction("CustomerDetails", new { message = editMessage });
        }
    }
}
