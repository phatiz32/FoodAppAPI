using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using myapi.Models;

namespace myapi.Data
{
    public static class SeedData
    {
        public static async Task SeedAdminAsync(UserManager<AppUser> userManager)
        {
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                var newAdmin = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Administrator",
                    PhoneNumber = "0327980000"
                };
                var result = await userManager.CreateAsync(newAdmin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }

        }
    }
}