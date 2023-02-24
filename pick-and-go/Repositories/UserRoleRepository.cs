﻿using Microsoft.AspNetCore.Identity;
using PickAndGo.Data;
using PickAndGo.ViewModels;
using System.Runtime.InteropServices;

namespace PickAndGo.Repositories
{
    public class UserRoleRepository
    {
        IServiceProvider serviceProvider;
        ApplicationDbContext _aspContext;



        public UserRoleRepository(IServiceProvider serviceProvider, ApplicationDbContext aspcontext)
        {
            this.serviceProvider = serviceProvider;
            this._aspContext = aspcontext;
        }

        public async Task<bool> AddUserRole(string email, string roleName)
        {
            var UserManager = serviceProvider
                .GetRequiredService<UserManager<IdentityUser>>();
            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                await UserManager.AddToRoleAsync(user, roleName);
            }
            return true;
        }

        public async Task<bool> RemoveUserRole(string email, string roleName)
        {
            var UserManager = serviceProvider
                .GetRequiredService<UserManager<IdentityUser>>();
            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                await UserManager.RemoveFromRoleAsync(user, roleName);
            }
            return true;
        }

        public async Task<IEnumerable<RoleVM>> GetUserRoles(string email)
        {
            var UserManager = serviceProvider
                .GetRequiredService<UserManager<IdentityUser>>();
            var user = await UserManager.FindByEmailAsync(email);
            var roles = await UserManager.GetRolesAsync(user);
            List<RoleVM> roleVMObjects = new List<RoleVM>();
            foreach (var item in roles)
            {
                roleVMObjects.Add(new RoleVM() { Id = item, RoleName = item });
            }
            return roleVMObjects;
        }

    }
}
