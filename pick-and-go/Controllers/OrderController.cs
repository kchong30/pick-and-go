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

        public IActionResult History(int customerId, string message)
        {
            if (message == null)
            {
                message = "";
            }

            OrderRepository or = new OrderRepository(_db);
            IQueryable<OrderHistoryVM> vm = or.BuildOrderHistoryVM(customerId);

            ViewData["Message"] = message;

            return View(vm);
        }

        public IActionResult Favorites(int customerId, string message)
        {
            if (message == null)
            {
                message = "";
            }

            FavoritesRepository fr = new FavoritesRepository(_db);
            IQueryable<FavoritesVM> vm = fr.BuildFavoritesVM(customerId);

            ViewData["Message"] = message;

            return View(vm);
        }

        [HttpPost]
        public IActionResult UpdateFavorites(int custId, int orderId, int lineId, Boolean isFavorite,
                                             string favoriteName, Boolean fromHistory)
        {
            var message = "";
            FavoritesRepository fr = new FavoritesRepository(_db);

            if (isFavorite)
            {
                message = fr.DeleteFavoritesRecord(custId, orderId, lineId);
            }
            else
            {
                message = fr.CreateFavoritesRecord(custId, orderId, lineId, favoriteName);
            }

            if (fromHistory)
            {
                return RedirectToAction("History", "Order", new
                {
                    customerId = custId,
                    message = message
                });
            }
            else
            {
                return RedirectToAction("Favorites", "Order", new
                {
                    customerId = custId,
                    message = message
                });
            }
        }

        [HttpPost]
        public IActionResult ChangeFavoritesName(int custId, int orderId, int lineId,
                                                 string favoriteName)
        {
            var message = "";
            FavoritesRepository fr = new FavoritesRepository(_db);

            message = fr.ChangeFavoritesRecord(custId, orderId, lineId, favoriteName);

            return RedirectToAction("Favorites", "Order", new
            {
                customerId = custId,
                message = message
            });
        }

        [HttpPost]
        public IActionResult AddFavoriteToOrder(int custId, int orderId, int lineId)
        {
            var message = "";

            return RedirectToAction("Favorites", "Order", new
            {
                customerId = custId,
                message = message
            });
        }
    }
}
