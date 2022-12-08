using Microsoft.AspNetCore.Mvc;
using pick_and_go.Data;
using pick_and_go.Models;
using pick_and_go.ViewModels;
using pick_and_go.Repositories;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace pick_and_go.Controllers
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
        public IActionResult Orders(string searchName, string searchOrder, bool changeStatus = false)
        {
            OrderRepository or = new OrderRepository(_db);

            if (changeStatus)
            {
                string editMessage = "";
                editMessage = or.UpdateOrderStatus(0, COMPLETED);

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