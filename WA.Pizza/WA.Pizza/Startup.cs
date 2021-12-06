using WA.Pizza.Api.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using WA.Pizza.Api.GlobalException;
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
            services.AddControllers();
            services.ConfigureServices();
            services.AddDbContext(Configuration);
            ServiceCollectionExtensions.AddControllersWithViews(services);

            MapperGlobal.Configure();

            services.AddScoped<IOrderDataService, OrderDataService>();
            services.AddScoped<IBasketDataService, BasketDataService>();
            services.AddScoped<ICatalogDataService, CatalogDataService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMiddleware<ErrorHandlerMiddleware>();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints();
        }
    }
}
