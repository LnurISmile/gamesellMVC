using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Identity
{
    public static class SeedIdendity
    {
        public static async Task Seed(UserManager<User> userManager,
                                RoleManager<IdentityRole> roleManager,
                                IConfiguration configuration)
        {
            var username = configuration["Data:AdminUser:username"];
            var email = configuration["Data:AdminUser:email"];
            var password = configuration["Data:AdminUser:password"];
            var role = configuration["Data:AdminUser:role"];

            if (await userManager.FindByNameAsync(username) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                await roleManager.CreateAsync(new IdentityRole("Customer"));
                await roleManager.CreateAsync(new IdentityRole("Moderator"));

                var user = new User()
                {
                    UserName = username,
                    Email = email,
                    FirstName = "admin",
                    LastName = "admin",
                    IsApproved = true,
                    EmailConfirmed = true,
                    languageID = 1,
                    currencyID = 1,
                    Balance = 1000
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Moderator");
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
