using Microsoft.AspNetCore.Mvc;
using PickAndGo.Data;
using PickAndGo.Models;
using PickAndGo.ViewModels;
using System.Diagnostics;
using PickAndGo.Repositories;

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

        public IActionResult Overview(string currentDate, string submitBtn)
        {
            OrderHeaderRepository ohRepo = new OrderHeaderRepository(_db);
            OrderHeaderVM ohVM = new OrderHeaderVM();

            if (currentDate == null)
            {
                currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            } else

            switch (submitBtn)
            {
                case ">":
                    currentDate = Convert.ToDateTime(currentDate).AddDays(1).ToString("yyyy-MM-dd");
                    break;
                case "<":
                    currentDate = Convert.ToDateTime(currentDate).AddDays(-1).ToString("yyyy-MM-dd");
                    break;
                default:
                    currentDate = currentDate.ToString();
                    break;
            }

            ViewBag.currentTime = DateTime.Now.ToString("h:mm:s tt");

            ohVM.Date = currentDate;
            ohVM.Outstanding = ohRepo.GetOverview(ohVM.Date).Item1;
            ohVM.Completed = ohRepo.GetOverview(ohVM.Date).Item2;

            return View(ohVM);
        }

    }
}