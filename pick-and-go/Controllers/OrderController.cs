using Microsoft.AspNetCore.Mvc;
using PickAndGo.Models;
using PickAndGo.Repositories;
using PickAndGo.ViewModels;

namespace PickAndGo.Controllers
{
    public class OrderController : Controller
    {
        private readonly PickAndGoContext _db;

        public OrderController(PickAndGoContext context)
        {
            _db = context;
        }

        public IActionResult Index(ProductVM products)
        {
            ProductRepository pr = new ProductRepository(_db);
            var vm = pr.GetProducts();
            return View(vm);
        }

        public IActionResult Customize(int SelectedProductId)
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }
    }
}
