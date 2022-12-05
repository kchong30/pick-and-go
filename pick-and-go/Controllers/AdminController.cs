using Microsoft.AspNetCore.Mvc;
using pick_and_go.Data;
using pick_and_go.Models;
using System.Diagnostics;

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
    }
}