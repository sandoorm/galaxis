using System;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using GalaxisProject_WebAPI;
using GalaxisProjectWebAPI.Infrastructure;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Galaxis_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables("")
                .Build();

            var url = config["ASPNETCORE_URLS"] ?? "http://*:8080";

            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(url)
                .Build();

            MigrateDbContext<ApplicationDbContext>(host);

            host.Run();
        }

        private static void MigrateDbContext<TContext>(IWebHost host)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<TContext>();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                    throw;
                }
            }
        }
    }
}