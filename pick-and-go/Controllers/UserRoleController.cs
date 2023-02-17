using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PickAndGo.Data;
using PickAndGo.Models;
using PickAndGo.Repositories;
using PickAndGo.ViewModels;

namespace PickAndGo.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {
        private ApplicationDbContext _aspContext;
        private PickAndGoContext _pgContext;
        private IServiceProvider _serviceProvider;

        public UserRoleController(ApplicationDbContext aspContext,
                                    IServiceProvider serviceProvider,
                                    PickAndGoContext pgContext)
        {
            _aspContext = aspContext;
            _serviceProvider = serviceProvider;
            _pgContext = pgContext;
        }

        public ActionResult Index()
        {
            UserRepository ur = new UserRepository(_aspContext, _pgContext);
            var users = ur.GetAllUsers();
            return View(users);
        }

        public async Task<IActionResult> Detail(string userName)
        {
            UserRoleRepository urr = new UserRoleRepository(_serviceProvider);
            var roles = await urr.GetUserRoles(userName);
            ViewBag.UserName = userName;
            return View(roles);
        }

        public ActionResult Create(string userName)
        {

            ViewBag.SelectedUser = userName;

            RoleRepository rl = new RoleRepository(_aspContext);
            var roles = rl.GetAllRoles().ToList();

 
            var preRoleList = roles.Select(r =>
                new SelectListItem { Value = r.RoleName, Text = r.RoleName })
                   .ToList();
  
            var roleList = new SelectList(preRoleList, "Value", "Text");

   
            ViewBag.RoleSelectList = roleList;

   
            var userList = _aspContext.Users.ToList();

            var preUserList = userList.Select(u => new SelectListItem
            { Value = u.Email, Text = u.Email }).ToList();
            SelectList userSelectList = new SelectList(preUserList
                                                      , "Value"
                                                      , "Text");

            ViewBag.UserSelectList = userSelectList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRoleVM userRoleVM)
        {
            UserRoleRepository urr = new UserRoleRepository(_serviceProvider);

            if (ModelState.IsValid)
            {
                var addUR = await urr.AddUserRole(userRoleVM.Email,
                                                            userRoleVM.Role);
            }
            try
            {
                return RedirectToAction("Detail", "UserRole",
                       new { userName = userRoleVM.Email });
            }
            catch
            {
                return View();
            }
        }

    }

}
