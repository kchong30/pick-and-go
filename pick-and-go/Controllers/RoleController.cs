using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PickAndGo.Data;
using PickAndGo.Repositories;
using PickAndGo.ViewModels;
using System.Data;

namespace PickAndGo.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            RoleRepository rr = new RoleRepository(_context);
            return View(rr.GetAllRoles());
        }

        public IActionResult Create()
        {
            RoleVM vm = new RoleVM();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(RoleVM vm)
        {

            RoleRepository rr = new RoleRepository(_context);
            rr.CreateRole(vm.RoleName);
            return RedirectToAction("Index", "Role");
        }

        public IActionResult Delete(string roleName)
        {
            RoleRepository rr = new RoleRepository(_context);
            rr.DeleteRole(roleName);
            return RedirectToAction("Index", "Role");
        }
    }

}
