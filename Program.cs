using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Diagnostics;

using GalaxisProject_WebAPI;
using GalaxisProjectWebAPI.Infrastructure;

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

            Console.WriteLine("fgretghzthzthzthtzhztht!!!!");

            MigrateDbContext<GalaxisDbContext>(host);
            
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
                    Console.WriteLine("!!!!!" + ex.Message);
                }
            }
        }
    }
}