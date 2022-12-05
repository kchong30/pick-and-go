using Microsoft.AspNetCore.Mvc;
using pick_and_go.Data;
using pick_and_go.Models;
using System.Diagnostics;

namespace pick_and_go.Controllers
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