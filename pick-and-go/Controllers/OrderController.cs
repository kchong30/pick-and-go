using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using PickAndGo.Models;
using PickAndGo.Repositories;
using PickAndGo.ViewModels;
using System.Net.NetworkInformation;
using System;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Newtonsoft.Json.Linq;

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

        public IActionResult Index(ProductVM products, string nameInput)
        {
            //If the user is a guest - set viewbag for greeting to nameInput (gathered from form at landing page).
            //If logged in, get customer's first name - pass on for greeting.
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.NameInput = nameInput;
                HttpContext.Session.SetString("firstName", nameInput);
            }
            else
            {
                CustomerRepository cr = new CustomerRepository(_db);
                var customer = cr.ReturnCustomerByEmail(User.Identity.Name);
                ViewBag.NameInput = customer.FirstName;
                HttpContext.Session.SetString("firstName", customer.FirstName);
            }
  

            ProductRepository pr = new ProductRepository(_db);
            var vm = pr.GetProducts();
            return View(vm);
        }

        public IActionResult Customize(int SelectedProductId)
        {
            // Receving Product ID from Main page
            IngredientsRepository ir = new IngredientsRepository(_db);
            IQueryable<IngredientListVM> iVm = ir.BuildIngredientListVM();

            ProductRepository pr = new ProductRepository(_db);
            IQueryable<ProductVM> pVm = pr.GetProducts();

            OrderCustomizeVM ocVm = new OrderCustomizeVM();
            ocVm.productVMs = pVm.ToList();
            ocVm.ingredientListVMs = iVm.ToList();

            string aa = HttpContext.Session.GetString("cart");
            ViewData["ProductId"] = SelectedProductId;
            return View(ocVm);
        }

        public IActionResult History(string message)
        {
            if (message == null)
            {
                message = "";
            }

            int customerId = Convert.ToInt32(HttpContext.Session.GetString("customerid"));

            OrderRepository or = new OrderRepository(_db, _configuration);
            IQueryable<OrderHistoryVM> vm = or.BuildOrderHistoryVM(customerId);

            ViewData["Message"] = message;

            return View(vm);
        }


        public IActionResult ShoppingCart()
        {
            // Retrieve the session string value
            string jsonData = HttpContext.Session.GetString("shoppingCart");

            // Pass it to VM for View
            List<ShoppingCartVM> items = JsonConvert.DeserializeObject<List<ShoppingCartVM>>(jsonData);

            // Check if the user is logged in or no

            if (User.Identity.Name != null)
            {
                string email = User.Identity.Name;
                CustomerRepository cR = new CustomerRepository(_db);
                // get current user record from client
                Customer customer = cR.ReturnCustomerByEmail(email);

                //if (User.Identity.IsAuthenticated)
                //{
                HttpContext.Session.SetString("firstName", customer.FirstName);
                HttpContext.Session.SetString("lastName", customer.LastName);
                HttpContext.Session.SetInt32("customerId", customer.CustomerId);

                //}
            }
            return View(items);

        }


        [HttpPost]
        public void StoreCart([FromBody] SessionVM data)
        {
            if (data.PickupTime != null)
            {
                HttpContext.Session.SetString("pickupTime", DateTime.Now.ToString());
                //HttpContext.Session.SetString("pickupTime", data.PickupTime);
            }
            if (data.CartJson != null)
            {
                HttpContext.Session.SetString("shoppingCart", data.CartJson);
            }

            if (data.Email != null)
            {
                HttpContext.Session.SetString("email", data.Email);
            }
        }


        // This method receives and stores
        // the Paypal transaction details.
        [HttpPost]
        public JsonResult PaySuccess([FromBody] IPN iPN)
        {
            // Retrieve the session string value
            string pickupTimeString = HttpContext.Session.GetString("pickupTime");
            // Convert the string to a DateTime object
            DateTime pickupTime = DateTime.Parse(pickupTimeString);
          
            string sandwichJson = HttpContext.Session.GetString("shoppingCart");

            //// Pass it to VM for View?
            //List<ShoppingCartVM> items = JsonConvert.DeserializeObject<List<ShoppingCartVM>>(jsonData);
            int customerId = HttpContext.Session.GetInt32("customerId") ?? 0;
            string firstName = HttpContext.Session.GetString("firstName");
            string lastName = HttpContext.Session.GetString("lastName");
            string email = iPN.email; // this is from the user input not from paypal data

            OrderRepository oR = new OrderRepository(_db, _configuration);

            decimal orderTotal = decimal.Parse(iPN.amount);
            if (User.Identity.Name != null)
            {
                email = User.Identity.Name;
            }
                oR.CreateOrder(customerId, firstName, lastName, pickupTime, iPN.paymentID, orderTotal,sandwichJson,email);

            // create order header, line... etc...

            return Json(iPN);
        }


        public IActionResult Confirmation(string confirmationId)
        {
            // show the order confirm page

            // place holder code
            var record =
            _db.OrderHeaders.Where(t => t.PaymentId == confirmationId).FirstOrDefault();
            return View("Confirmation", record);
        }

        public IActionResult Favorites(string message)
        {
            if (message == null)
            {
                message = "";
            }

            int customerId = Convert.ToInt32(HttpContext.Session.GetString("customerid"));

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
