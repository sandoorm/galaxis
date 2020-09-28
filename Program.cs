using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Collections.Generic;

using GalaxisProject_WebAPI;
using GalaxisProjectWebAPI.Infrastructure;
using GalaxisProjectWebAPI.Model.DummyDataFactory;
using GalaxisProjectWebAPI.DataModel;
using GalaxisProjectWebAPI.Model.Converters;

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
                CreateDummyDataIfNeeded(dbContext);
            }
        }

        private static void CreateDummyDataIfNeeded<TContext>(TContext galaxisContext) where TContext : DbContext
        {  
            var dummyDataStorage = new DummyDataStorage(galaxisContext);

            dummyDataStorage.SaveDummyDatas(new DummyCompanyFactory());
            dummyDataStorage.SaveDummyDatas(new DummyFundFactory());
            dummyDataStorage.SaveDummyDatas(new DummyTokenFactory());
            dummyDataStorage.SaveDummyDatas(new DummyTokenPriceHistoricDataFactory(new TokenPriceHistoricDataConverter()));

            AddDummyFundTokenData(galaxisContext);
        }

        private static void AddDummyFundTokenData(DbContext galaxisContext)
        {
            var dummyFundTokens = new List<FundToken>
            {
                new FundToken
                {
                    FundId = 1,
                    TokenId = 1,
                    Quantity = 3,
                    Timestamp = 1601120674
                },
                new FundToken
                {
                    FundId = 1,
                    TokenId = 1,
                    Quantity = 2,
                    Timestamp = 1601120664
                }
            };

            galaxisContext.Set<FundToken>().AddRange(dummyFundTokens);
            galaxisContext.SaveChanges();
        }
    }
}