using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using PickAndGo.Models;
using PickAndGo.Repositories;
using PickAndGo.ViewModels;
using System.Net.NetworkInformation;
using System;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;


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

        public IActionResult ShoppingCart()
        {


            // Retrieve JSON data
                        string jsonData = @"[
                {
                    ""productId"": ""1"",
                    ""description"": ""Small Sandwich"",
                    ""ingredient"": [
                        {
                            ""ingredientId"": 2,
                            ""description"": ""Wheat Bread"",
                            ""quantity"": ""1""
                        },
                        {
                            ""ingredientId"": 6,
                            ""description"": ""Tomato"",
                            ""quantity"": ""2""
                        },
                        {
                            ""ingredientId"": 8,
                            ""description"": ""Cucumber"",
                            ""quantity"": ""1""
                        }
                    ],
                    ""subtotal"": ""2.00""
                },
                {
                    ""productId"": ""1"",
                    ""description"": ""Large Sandwich"",
                    ""ingredient"": [
                        {
                            ""ingredientId"": 1,
                            ""description"": ""Italian Bread"",
                            ""quantity"": ""1""
                        },
                        {
                            ""ingredientId"": 5,
                            ""description"": ""Ham"",
                            ""quantity"": ""2""
                        },
                        {
                            ""ingredientId"": 8,
                            ""description"": ""Cucumber"",
                            ""quantity"": ""2""
                        }
                    ],
                    ""subtotal"": ""5.00""
                }
            ]";

            // Deserialize JSON into C# object
            // Retrieve the session string value
            string shoppingCart = HttpContext.Session.GetString("shoppingCart");
            List<ShoppingCartVM> items = JsonConvert.DeserializeObject<List<ShoppingCartVM>>(jsonData);

            // Pass C# object to Razor view
            return View(items);


        }

        [HttpPost]
        public void StoreCart([FromBody] SessionVM data)
        {
            HttpContext.Session.SetString("pickupTime", data.PickupTime);
            //HttpContext.Session.SetString("shoppingCart", data.CartJson);
        }


        // This method receives and stores
        // the Paypal transaction details.
        [HttpPost]
        public JsonResult PaySuccess([FromBody] IPN iPN )
        {
            // Retrieve the session string value
            string pickupTime = HttpContext.Session.GetString("pickupTime");

            // Convert the string to a DateTime object
            DateTime dateTimeValue = DateTime.Parse(pickupTime);


            // we do not create an IPN record, we will have order
            // need to call Steph's code for creting order record
            // need to pass customer Id and name as well

            return Json(iPN);
        }


        // Home page shows list of items.
        // Item price is set through the ViewBag.
        public IActionResult Confirmation(string confirmationId)
        {
            // show the payment success page? maybe?

            var record =
            _db.OrderHeaders.Where(t => t.PaymentId == confirmationId).FirstOrDefault();


            return View("Confirmation", record);
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

           

            return RedirectToAction("Favorites", "Order", new
            {
                customerId = custId,
                message = message
            });
        }
    }
}
