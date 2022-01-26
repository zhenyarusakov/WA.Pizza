using WA.Pizza.Api.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.MapperConfiguration;


namespace WA.Pizza.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSwagger()
                .AddDbContext(Configuration)
                .AddControllersOptions();

            var connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<WAPizzaContext>(
                options => options.UseSqlServer(connection));


            MapperGlobal.Configure();

            services.AddScoped<IOrderDataService, OrderDataService>();
            services.AddScoped<IBasketDataService, BasketDataService>();
            services.AddScoped<ICatalogDataService, CatalogDataService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();

            using var serviceScope = app.ApplicationServices.CreateScope();
            var appDbContext = serviceScope.ServiceProvider.GetRequiredService<WAPizzaContext>();
            appDbContext.Database.Migrate();

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints();
        }
    }
}
