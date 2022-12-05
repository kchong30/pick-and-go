using Microsoft.AspNetCore.Mvc;
using pick_and_go.Models;
using System.Diagnostics;

namespace pick_and_go.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}