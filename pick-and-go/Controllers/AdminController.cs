using Microsoft.AspNetCore.Mvc;
using pick_and_go.Data;
using pick_and_go.ViewModels;
using pick_and_go.Models;
using System.Diagnostics;
using pick_and_go.Repository;

namespace pick_and_go.Controllers
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

        public IActionResult Overview()
        {
            OrderHeaderRepository ohRepo = new OrderHeaderRepository(_db);
            OrderHeaderVM ohVM = new OrderHeaderVM();

            ohVM.Pending = ohRepo.GetAll().Item1;
            ohVM.Completed = ohRepo.GetAll().Item2;

            return View(ohVM);
        }
    }
}