using Microsoft.AspNetCore.Mvc;
using PickAndGo.Data;
using PickAndGo.Models;
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
            return View();
        }


    }
}