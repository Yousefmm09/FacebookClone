using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static  async Task AddRole(RoleManager<IdentityRole> _roleManager)
        {
            var user = _roleManager.Roles.Count();
            if(user ==0)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));

                if (!await _roleManager.RoleExistsAsync("User"))
                    await _roleManager.CreateAsync(new IdentityRole("User"));
            }
        }
    }
}
