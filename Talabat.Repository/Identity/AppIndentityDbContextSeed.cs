using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIndentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "MohamedHesham",
                    Email = "muhamedhesham632@gmail.com",
                    UserName = "muhamedhesham632",
                    PhoneNumber = "01153442908"
                };
                await userManager.CreateAsync(user , "Pa$$w0rd");
            }
        }
    }
}
