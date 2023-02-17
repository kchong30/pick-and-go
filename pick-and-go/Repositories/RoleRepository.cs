using Microsoft.AspNetCore.Identity;
using PickAndGo.Data;
using PickAndGo.Models;
using PickAndGo.ViewModels;

namespace PickAndGo.Repositories
{
    public class RoleRepository
    {
        ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            this._context = context;
            var rolesCreated = CreateInitialRoles();
        }

        public List<RoleVM> GetAllRoles()
        {
            var roles = _context.Roles;
            List<RoleVM> roleList = new List<RoleVM>();

            foreach (var item in roles)
            {
                roleList.Add(new RoleVM()
                {
                    RoleName = item.Name
                                          ,
                    Id = item.Id
                });
            }
            return roleList;
        }

        public RoleVM GetRole(string roleName)
        {
            var role = _context.Roles.Where(r => r.Name == roleName)
                                     .FirstOrDefault();
            if (role != null)
            {
                return new RoleVM()
                {
                    RoleName = role.Name
                                    ,
                    Id = role.Id
                };
            }
            return null;
        }

        public bool CreateRole(string roleName)
        {
            var role = GetRole(roleName);
            if (role != null)
            {
                return false;
            }
            _context.Roles.Add(new IdentityRole
            {
                Name = roleName,
                Id = roleName,
                NormalizedName = roleName.ToUpper()
            });
            _context.SaveChanges();
            return true;
        }

        public void DeleteRole(string roleName)
        {
            var role = _context.Roles.Where(r => r.Name == roleName)
                         .FirstOrDefault();
            _context.Roles.Remove(role);
            _context.SaveChanges();
        }

        public bool CreateInitialRoles()
        {
            string[] roleNames = { "Admin", "Customer" };
            foreach (var roleName in roleNames)
            {
                var created = CreateRole(roleName);
 
                if (!created)
                {
                    return false;
                }
            }
            return true;
        }


    }
}
