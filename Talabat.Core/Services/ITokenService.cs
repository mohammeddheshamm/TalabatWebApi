﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Core.Services
{
    public interface ITokenService
    {
        // JWT is a55tsar for Json Web Token
        Task<string> CreateToken(AppUser user ,UserManager<AppUser> userManager);
    }
}
