﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace TalabatAPIS.Extentions
{
    public static class UserManagerExtensions
    {
        public async static Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.Include(U => U.Address).SingleOrDefaultAsync(U => U.Email == email);

            return user;
        }
    }
}
