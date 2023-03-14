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
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.SignalR.Protocol;
using NuGet.Protocol.Core.Types;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PickAndGo.Controllers
{
    //[Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        private readonly PickAndGoContext _db;
        private readonly IConfiguration _configuration;

        public const string OUTSTANDING = "O";
        public const string COMPLETED = "C";

        public AdminController(PickAndGoContext context, IConfiguration configuration)
        {
            _db = context;
            _configuration = configuration;
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

        public IActionResult IngredientsDetails(int id, string message)
        {
            IngredientsRepository iR = new IngredientsRepository(_db);
            var vm = iR.ReturnIngredientById(id);

            ViewData["Message"] = message;

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
            string editMessage = "";

            IngredientsRepository iR = new IngredientsRepository(_db);
            if (ModelState.IsValid)
            {
                editMessage = iR.EditIngredient(ingredientVM);
            }
            return RedirectToAction("IngredientsDetails", "Admin", new { id = ingredientVM.IngredientId,
                                                                         message = editMessage
            });
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

        public IActionResult Overview(string currentDate, string submitBtn)
        {
            OrderHeaderRepository ohRepo = new OrderHeaderRepository(_db);
            OrderHeaderVM ohVM = new OrderHeaderVM();

            if (currentDate == null)
            {
                currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else

                switch (submitBtn)
                {
                    case ">":
                        currentDate = Convert.ToDateTime(currentDate).AddDays(1).ToString("yyyy-MM-dd");
                        break;
                    case "<":
                        currentDate = Convert.ToDateTime(currentDate).AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                    default:
                        currentDate = currentDate.ToString();
                        break;
                }

            ViewBag.currentTime = DateTime.Now.ToString("h:mm:s tt");

            ohVM.Date = currentDate;
            var tuple = ohRepo.GetOverviewCounts(ohVM.Date);
            ohVM.Outstanding = tuple.Item1;
            ohVM.Completed = tuple.Item2;

            var tuple2 = ohRepo.GetOverviewValues(ohVM.Date);
            ohVM.OutstandingVal = tuple2.Item1;
            ohVM.CompletedVal = tuple2.Item2;

            CustomerRepository cr = new CustomerRepository(_db);
            var tuple3 = cr.GetOverviewCustomers();
            ohVM.Accounts = tuple3.Item1;
            ohVM.Guests = tuple3.Item2;

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

            OrderRepository or = new OrderRepository(_db, _configuration);
            IQueryable<OrderListVM> vm = or.BuildOrderListVM(orderFilter, "", "");

            ViewData["Message"] = message;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Orders(string searchName, string searchOrder, int orderId, int lineId,
                                    Boolean changeStatus)
        {
            OrderRepository or = new OrderRepository(_db, _configuration);

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

        public IActionResult Transactions()
        {
            DateTime fromDate = new DateTime(2022, 1, 1); 
            DateTime toDate = DateTime.Today;

            ViewData["CurrentFromDate"] = fromDate;
            ViewData["CurrentToDate"] = toDate;

            OrderRepository or = new OrderRepository(_db, _configuration);
            IQueryable<OrderTransactionVM> vm = or.BuildOrderTransactionVM("", "", fromDate, toDate);

            ViewData["CurrentNameSearch"] = "";
            ViewData["CurrentOrderSearch"] = "";

            return View(vm);
        }

        [HttpPost]
        public IActionResult Transactions(string searchName, string searchOrder,
                                          string fromDate, string toDate)
        {
            OrderRepository or = new OrderRepository(_db, _configuration);

            DateTime fromDateValue = string.IsNullOrEmpty(fromDate) ? new DateTime(2022, 1, 1) : DateTime.Parse(fromDate);
            DateTime toDateValue = string.IsNullOrEmpty(toDate) ? DateTime.Today : DateTime.Parse(toDate);

            ViewData["CurrentNameSearch"] = searchName;
            ViewData["CurrentOrderSearch"] = searchOrder;
            ViewData["CurrentFromDate"] = fromDateValue;
            ViewData["CurrentToDate"] = toDateValue;

            IQueryable<OrderTransactionVM> vm = or.BuildOrderTransactionVM(searchName, searchOrder,
                                                                           fromDateValue, toDateValue);

            return View(vm);
        }
    }
}