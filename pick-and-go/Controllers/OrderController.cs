﻿using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using PickAndGo.Models;
using PickAndGo.Repositories;
using PickAndGo.ViewModels;
using PickAndGo.Services;
using System.Net.NetworkInformation;
using System;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Newtonsoft.Json.Linq;
using System.Data;
using NuGet.Protocol.Core.Types;
using PickAndGo.Utilities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PickAndGo.Controllers
{
    public class OrderController : Controller
    {
        private readonly PickAndGoContext _db;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;


        public OrderController(PickAndGoContext context, IConfiguration configuration, IEmailService emailService)
        {
            _db = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        public IActionResult Index(ProductVM products)
        {
            //If the user is a guest - set viewbag for greeting to nameInput (gathered from form at landing page).
            //If logged in, get customer's first name - pass on for greeting.
            if (HttpContext.Session.GetString("firstName") == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    CustomerRepository cr = new CustomerRepository(_db);
                    var customer = cr.ReturnCustomerByEmail(User.Identity.Name);
                    ViewBag.NameInput = customer.FirstName;
                    HttpContext.Session.SetString("firstName", customer.FirstName);
                }

            }
            else
            {
                ViewBag.NameInput = HttpContext.Session.GetString("firstName");

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

        public IActionResult EditCustomize(int Index, string removeItem)
        {
            // Receving Product ID from Main page
            IngredientsRepository ir = new IngredientsRepository(_db);
            IQueryable<IngredientListVM> iVm = ir.BuildIngredientListVM();

            ProductRepository pr = new ProductRepository(_db);
            IQueryable<ProductVM> pVm = pr.GetProducts();

            OrderCustomizeVM ocVm = new OrderCustomizeVM();
            ocVm.productVMs = pVm.ToList();
            ocVm.ingredientListVMs = iVm.ToList();

            string cartItem = HttpContext.Session.GetString("shoppingCart");
            var json = JsonConvert.DeserializeObject<List<ShoppingCartVM>>(cartItem);

            // localStorage delete
            // Session Variable delete

            //ViewData["ProductId"] = SelectedProductId;
            ViewData["cartItem"] = json[Index];
            ViewData["Index"] = Index;
            ViewData["RemoveItem"] = removeItem;

            return View(ocVm);
        }

        public IActionResult History(string message, int customerId, int? page)
        {
            if (message == null)
            {
                message = "";
            }

            if (customerId == 0)
            {
                customerId = Convert.ToInt32(HttpContext.Session.GetString("customerid"));
            }

            if (page == null || page == 0)
            {
                page = 1;
            }

            CustomerRepository cr = new CustomerRepository(_db);
            var customer = cr.ReturnCustomerById(customerId);
            ViewData["CustomerName"] = customer.FirstName + " " + customer.LastName;

            OrderRepository or = new OrderRepository(_db, _configuration);
            IQueryable<OrderHistoryVM> vm = or.BuildOrderHistoryVM(customerId);

            int pageSize = 10;

            ViewData["Message"] = message;
           
            return View(PaginatedList<OrderHistoryVM>.Create(vm.AsNoTracking()
                                                         , page ?? 1, pageSize));
        }

        public IActionResult ShoppingCart()
        {
            // Retrieve the session string value
            string jsonData = HttpContext.Session.GetString("shoppingCart");

            if(jsonData?.Length > 0)
       
            {
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

            return View();



        }


        [HttpPost]
        public void StoreCart([FromBody] SessionVM data)
        {
            if (data.PickupTime != null)
            {
                HttpContext.Session.SetString("pickupTime", data.PickupTime);
            }
            if (data.CartJson != null)
            {
                HttpContext.Session.SetString("shoppingCart", data.CartJson);
            }

            if (data.Email != null)
            {
                HttpContext.Session.SetString("email", data.Email);
            }

            if (data.FirstName != null)
            {
                HttpContext.Session.SetString("firstName", data.FirstName);
            }
        }


        // This method receives and stores
        // the Paypal transaction details.
        [HttpPost]
        public JsonResult PaySuccess([FromBody] IPN iPN)
        {
            var message = "";
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
                HttpContext.Session.SetString("email", email);
            }
            message = oR.CreateOrder(customerId, firstName, lastName, pickupTime, iPN.paymentID,
                                     orderTotal, sandwichJson, email);

            var response = _emailService.SendConfirmationEmail(new ConfirmationEmailModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PickUpTime = pickupTimeString,
                OrderDetails = sandwichJson
            }) ;

            ViewData["Message"] = message;

            return Json(iPN);
        }

        public IActionResult Confirmation(string confirmationId)
        {
            // show the order confirm page

            // place holder code

            OrderRepository or = new OrderRepository(_db, _configuration);
            var record = or.GetConfirmationInfo(confirmationId);
            
            return View("Confirmation", record);
        }

        public IActionResult Favorites(string message)
        {
            if (message == null)
            {
                message = "";
            }

            int customerId = Convert.ToInt32(HttpContext.Session.GetString("customerid"));
            CustomerRepository cr = new CustomerRepository(_db);
            var customer = cr.ReturnCustomerById(customerId);
            ViewData["CustomerName"] = customer.FirstName + " " + customer.LastName;

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
    }
}
