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
            IngredientsRepository iR = new IngredientsRepository(_db);
            var vm = iR.BuildIngredientVM(0);
            ViewData["categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryId");
            return View();
        }

        [HttpPost]
        public IActionResult IngredientsCreate(IngredientVM ingredient)
        {
            string addMessage = "";

            IngredientsRepository iR = new IngredientsRepository(_db);
            if (ModelState.IsValid)
            {
                addMessage = iR.CreateIngredient(ingredient);
            }

            ViewData["categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryId");
            return RedirectToAction("Ingredients", "Admin", new { message = addMessage });
          
        }

        public IActionResult IngredientsDetails(int id)
        {
            IngredientsRepository iR = new IngredientsRepository(_db);
            var vm = iR.ReturnIngredientById(id);
            return View(vm);
        }

        public IActionResult IngredientsEdit(int id)
        {
            IngredientsRepository iR = new IngredientsRepository(_db);
            var vm = iR.BuildIngredientVM(id);

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