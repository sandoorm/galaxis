using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using GalaxisProjectWebAPI;
using GalaxisProjectWebAPI.Infrastructure;
using GalaxisProjectWebAPI.Model;

namespace GalaxisProject_WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // localhost connection with the provided PostgreSQL docker-compose.yml
            // => "User Id=galaxis;Password=galaxis;Server=localhost;Port=5432;Database=galaxis;"
            services.AddDbContext<GalaxisDbContext>(options =>
                options.UseNpgsql(EnvVarHelper.GetGalaxisDbConnectionString()));

            services.AddScoped<IFundRepository, FundRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IPriceHistoryRepository, PriceHistoryRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Galaxis backend WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Galaxis backend WebAPI v1");
            });
        }
    }
}