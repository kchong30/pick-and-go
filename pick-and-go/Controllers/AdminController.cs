using Microsoft.AspNetCore.Mvc;
using PickAndGo.Data;
using PickAndGo.Models;
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
    }
}