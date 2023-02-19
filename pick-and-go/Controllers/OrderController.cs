using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using PickAndGo.Models;
using PickAndGo.Repositories;
using PickAndGo.ViewModels;
using System.Net.NetworkInformation;
using System;
using Newtonsoft.Json;


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

        public IActionResult History(int customerId)
        {
            OrderRepository or = new OrderRepository(_db);
            IQueryable<OrderHistoryVM> vm = or.BuildOrderHistoryVM(customerId);

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
                    ""subtotal"": ""13.50""
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
                    ""subtotal"": ""15.20""
                }
            ]";

            // Deserialize JSON into C# object
            
            List<ShoppingCartVM> items = JsonConvert.DeserializeObject<List<ShoppingCartVM>>(jsonData);

            // Pass C# object to Razor view
            return View(items);


        }


    }
}
