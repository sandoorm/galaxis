using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using GalaxisProject_WebAPI;
using GalaxisProjectWebAPI.Infrastructure;
using GalaxisProjectWebAPI.Model.DummyDataFactory;

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

            MigrateDbContext<GalaxisDbContext>(host);
            
            host.Run();
        }

        private static void MigrateDbContext<TContext>(IWebHost host)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var dbContext = services.GetRequiredService<TContext>();
                dbContext.Database.Migrate();
                CreateDumyDataIfNeeded(dbContext);
            }
        }

        private static void CreateDumyDataIfNeeded<TContext>(TContext galaxisContext) where TContext : DbContext
        {  
            var dummyDataStorage = new DummyDataStorage(galaxisContext);

            dummyDataStorage.SaveDummyDatas(new DummyCompanyFactory());
            dummyDataStorage.SaveDummyDatas(new DummyFundFactory());
            dummyDataStorage.SaveDummyDatas(new DummyTokenFactory());
        }
    }
}