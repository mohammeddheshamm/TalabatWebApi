using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace WebAPIProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Hina anta msekt al kestrel 
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            //// b Get all services that work in scoped
            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = services.GetRequiredService<StoreDbContext>();

                await context.Database.MigrateAsync(); //Update Database
                
                await StoreContextSeed.SeedAsync(context, loggerFactory);
                
                var IdentityContext = services.GetRequiredService<AppIdentityDbContext>();
                
                await IdentityContext.Database.MigrateAsync();

                var userManager = services.GetRequiredService<UserManager<AppUser>>();

                await AppIndentityDbContextSeed.SeedUserAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex , "An error occured during Migration.");
            }
            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
