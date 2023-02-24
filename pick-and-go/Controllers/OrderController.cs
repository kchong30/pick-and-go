using Microsoft.AspNetCore.Mvc;
using PickAndGo.Models;
using PickAndGo.Repositories;
using PickAndGo.ViewModels;

namespace PickAndGo.Controllers
{
    public class OrderController : Controller
    {
        private readonly PickAndGoContext _db;
        private readonly IConfiguration _configuration;

        public OrderController(PickAndGoContext context, IConfiguration configuration)
        {
            _db = context;
            _configuration = configuration;
        }

        public IActionResult Index(ProductVM products)
        {
            ProductRepository pr = new ProductRepository(_db);
            var vm = pr.GetProducts();
            return View(vm);
        }

        public IActionResult Customize()
        {
            // Receving Product ID from Main page
            IngredientsRepository ir = new IngredientsRepository(_db);
            IQueryable<IngredientListVM> iVm = ir.BuildIngredientListVM();

            ProductRepository pr = new ProductRepository(_db);
            IQueryable<ProductVM> pVm = pr.GetProducts();

            OrderCustomizeVM ocVm = new OrderCustomizeVM();
            ocVm.productVMs = pVm.ToList();
            ocVm.ingredientListVMs = iVm.ToList();

            return View(ocVm);
            }

        public IActionResult History(int customerId, string message)
        {
            if (message == null)
            {
                message = "";
            }

            OrderRepository or = new OrderRepository(_db, _configuration);
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

            // create session object for this product and add to cart object,
            // redirect to shopping cart page?

            
            //
            // testing purposes only
            //

            return RedirectToAction("Favorites", "Order", new
            {
                customerId = custId,
                message = message
            });
        }
    }
}
