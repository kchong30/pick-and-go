using Microsoft.AspNetCore.Mvc;
using PickAndGo.Data;
using PickAndGo.Models;
using PickAndGo.ViewModels;
using System.Diagnostics;
using PickAndGo.Repositories;
using NuGet.Protocol;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.SignalR.Protocol;
using NuGet.Protocol.Core.Types;
using System.Security.Principal;

namespace PickAndGo.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        private readonly PickAndGoContext _db;

        public const string OUTSTANDING = "O";
        public const string COMPLETED = "C";

        public AdminController(PickAndGoContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ingredients(string message)
        {
            if (message == null)
            {
                message = "";
            }

            IngredientsRepository ir = new IngredientsRepository(_db);
            IQueryable<IngredientListVM> vm = ir.BuildIngredientListVM();

            ViewData["Message"] = message;

            return View(vm);
        }

        public IActionResult IngredientsCreate()
        {
            ViewData["categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryId");
            return View();
        }

        [HttpPost]
        public IActionResult IngredientsCreate(Ingredient ingredient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Ingredients.Add(new Ingredient
                    {
                        Description = ingredient.Description,
                        Price = ingredient.Price,
                        CategoryId = ingredient.CategoryId,
                        InStock = ingredient.InStock
                    });
                    _db.SaveChanges();

                    var addMessage = $"** Ingredient {ingredient.Description} has been added " +
                                      $"to category {ingredient.CategoryId}.";

                    return RedirectToAction("Ingredients", "Admin", new { message = addMessage });
                }
            }
            catch
            {
                return View();
            }

            ViewData["categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryId");
            return View(ingredient);
        }

        public IActionResult IngredientsDetails(int id)
        {
            IngredientsRepository iR = new IngredientsRepository(_db);
            var vm = iR.ReturnIngredientById(id);
            vm.InStockIcon = (vm.InStock == "Y") ? "check.svg" : "x.svg";
            return View(vm);
        }

        public IActionResult IngredientsEdit(int id)
        {
            IngredientsRepository iR = new IngredientsRepository(_db);
            var i = iR.GetIngredientRecord(id);

            IngredientVM vm = new IngredientVM();
            vm.IngredientId = i.IngredientId;
            vm.Description = i.Description;
            vm.Price = i.Price;
            vm.CategoryId = i.CategoryId;
            vm.IngredientInStock = i.InStock == "Y" ? true : false;

            ViewData["categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryId");
            return View(vm);
        }

        [HttpPost]
        public IActionResult IngredientsEdit(IngredientVM ingredientVM)
        {
            IngredientsRepository iR = new IngredientsRepository(_db);
            if (ModelState.IsValid)
            {
                iR.EditIngredient(ingredientVM);
            }
            return RedirectToAction("IngredientsDetails", "Admin", new { id = ingredientVM.IngredientId });
        }

        public IActionResult IngredientsDelete(int id)
        {
            IngredientsRepository iR = new IngredientsRepository(_db);
            IngredientVM ingredient = iR.ReturnIngredientById(id);
            ViewData["categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryId");
            return View(ingredient);
        }

        [HttpPost]
        public IActionResult IngredientsDelete(int id, string category)
        {
            IngredientsRepository iR = new IngredientsRepository(_db);

            string deleteMessage = "";

            deleteMessage = iR.DeleteIngredient(id, category);

            return RedirectToAction("Ingredients", "Admin", new { message = deleteMessage });
        }

        public IActionResult CustomerList()
        {
            CustomerRepository cR = new CustomerRepository(_db);
            var vm = cR.ReturnAllCustomers();

            return View(vm);
        }
        public IActionResult CustomerDetail(int customerId)
        {
            CustomerRepository cR = new CustomerRepository(_db);
            var vm = cR.ReturnCustomerById(customerId);

            return View(vm);
        }

        public IActionResult Overview()
        {
            OrderHeaderRepository ohRepo = new OrderHeaderRepository(_db);
            OrderHeaderVM ohVM = new OrderHeaderVM();

            ohVM.Outstanding = ohRepo.GetAll().Item1;
            ohVM.Completed = ohRepo.GetAll().Item2;

            return View(ohVM);
        }

        public IActionResult Orders(string message)
        {
            if (message == null)
            {
                message = "";
            }

            var orderFilter = "O";

            ViewData["CurrentFilter"] = orderFilter;
            ViewData["CurrentNameSearch"] = "";
            ViewData["CurrentOrderSearch"] = "";

            OrderRepository or = new OrderRepository(_db);
            IQueryable<OrderListVM> vm = or.BuildOrderListVM(orderFilter, "", "");

            ViewData["Message"] = message;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Orders(string searchName, string searchOrder, int orderId, int lineId,
                                    Boolean changeStatus)
        {
            OrderRepository or = new OrderRepository(_db);

            if (changeStatus)
            {
                string editMessage = "";
                editMessage = or.UpdateOrderLineStatus(orderId, lineId, COMPLETED);

                return RedirectToAction("Orders", "Admin", new { message = editMessage });
            }

            var orderFilter = Request.Form["orderfilter"].ToString();

            if (orderFilter == null || orderFilter == "")
            {
                orderFilter = "O";
            }

            ViewData["CurrentFilter"] = orderFilter;
            ViewData["CurrentNameSearch"] = searchName;
            ViewData["CurrentOrderSearch"] = searchOrder;

            IQueryable<OrderListVM> vm = or.BuildOrderListVM(orderFilter, searchName, searchOrder);

            return View(vm);
        }
    }
}