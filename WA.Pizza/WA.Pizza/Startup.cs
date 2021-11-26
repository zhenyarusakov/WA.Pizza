using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WA.Pizza.Api.Extensions;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.Data.MapperConfiguration;
using WA.Pizza.Infrastructure.Data.Services;

namespace WA.Pizza.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureServicesExtension();
            services.AddDbContextExtensions(Configuration);
            services.AddControllersWithViewsExtensions();

            MapperGlobal.Configure();

            services.AddScoped<IOrderDataService, OrderDataService>();
            services.AddScoped<IBasketDataService, BasketDataService>();
            services.AddScoped<ICatalogDataService, CatalogDataService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WA.Pizza v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
