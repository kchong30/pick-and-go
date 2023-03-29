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
using PickAndGo.Utilities;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;

namespace PickAndGo.Controllers
{
    [Authorize(Roles = "Admin")]

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

            OrderHeaderRepository ohRepo = new OrderHeaderRepository(_db);
            OverviewVM ov = ohRepo.GetOverviewCounts(currentDate);

            return View(ov);
        }

        public IActionResult Orders(string message, string orderFilter, string searchName, string searchOrder, int? page)
        {
            if (message == null)
            {
                message = "";
            }

            if (orderFilter == null)
            {
                orderFilter = OUTSTANDING;
            }

            if (searchName == null)
            {
                searchName = "";
            }

            if (searchOrder == null)
            {
                searchOrder = "";
            }

            ViewData["CurrentFilter"] = orderFilter;
            ViewData["CurrentNameSearch"] = searchName;
            ViewData["CurrentOrderSearch"] = searchOrder;

            if (page == null || page == 0)
            {
                page = 1;
            }

            OrderRepository or = new OrderRepository(_db, _configuration);
            IQueryable<OrderListVM> vm = or.BuildOrderListVM(orderFilter, "", "");

            ViewData["Message"] = message;

            int pageSize = 10;
               return View(PaginatedList<OrderListVM>.Create(vm.AsNoTracking()
                                                             , page ?? 1, pageSize));
        }
     
        [HttpPost]
        public IActionResult Orders(string searchName, string searchOrder, int orderId, int lineId,
                                    Boolean changeStatus, int? page)
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
                orderFilter = OUTSTANDING;
            }

            ViewData["CurrentFilter"] = orderFilter;
            ViewData["CurrentNameSearch"] = searchName;
            ViewData["CurrentOrderSearch"] = searchOrder;

            IQueryable<OrderListVM> vm = or.BuildOrderListVM(orderFilter, searchName, searchOrder);

            int pageSize = 10;

            return View(PaginatedList<OrderListVM>.Create(vm.AsNoTracking()
                                                          , page ?? 1, pageSize));

        }

        [HttpGet]
        [HttpPost]
        public IActionResult Transactions(string searchName, string searchOrder,
                                          string dateFrom, string dateTo, int? page)
        {
            if (searchName == null)
            {
                searchName = "";
            }

            if (searchOrder == null)
            {
                searchOrder = "";
            }

            ViewData["CurrentNameSearch"] = searchName;
            ViewData["CurrentOrderSearch"] = searchOrder;

            DateTime fromDate = string.IsNullOrEmpty(dateFrom) ? new DateTime(2022, 1, 1) : DateTime.Parse(dateFrom);
            DateTime toDate = string.IsNullOrEmpty(dateTo) ? DateTime.Today : DateTime.Parse(dateTo);

            ViewData["CurrentFromDate"] = fromDate.ToString();
            ViewData["CurrentToDate"] = toDate.ToString();

            if (page == null || page == 0)
            {
                page = 1;
            }            

            OrderRepository or = new OrderRepository(_db, _configuration);
            IQueryable<OrderTransactionVM> vm = or.BuildOrderTransactionVM(searchName, searchOrder,
                                                                           fromDate, toDate);

            int pageSize = 10;

            return View(PaginatedList<OrderTransactionVM>.Create(vm.AsNoTracking()
                                                         , page ?? 1, pageSize));
        }

        public IActionResult Products(string message)
        {
            if (message == null)
            {
                message = "";
            }

            ProductRepository pr = new ProductRepository(_db);
            var vm = pr.GetProducts();

            ViewData["Message"] = message;

            return View(vm);
        }

        public IActionResult ProductDetails(int productId, string message)
        {
            ProductRepository pr = new ProductRepository(_db);
            var vm = pr.ReturnProductById(productId);

            ViewData["Message"] = message;

            return View(vm);
        }

        public IActionResult ProductEdit(int productId)
        {
            ProductRepository pr = new ProductRepository(_db);
            var vm = pr.ReturnProductById(productId);

            return View(vm);
        }

        [HttpPost]
        public IActionResult ProductEdit(Product product)
        {
            string editMessage = "";

            ProductRepository pr = new ProductRepository(_db);
            if (ModelState.IsValid)
            {
                editMessage = pr.EditProduct(product);
            }
            return RedirectToAction("ProductDetails", "Admin", new
            {
                productId = product.ProductId,
                message = editMessage
            });
        }
    }
}