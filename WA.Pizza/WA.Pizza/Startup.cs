using Hangfire;
using WA.Pizza.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.Data.Services;
using Microsoft.Extensions.DependencyInjection;
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
                .AddControllersOptions()
                .AddHangfireServer()
                .AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnectionDb")))
                .AddScoped<IOrderDataService, OrderDataService>()
                .AddScoped<IBasketDataService, BasketDataService>()
                .AddScoped<ICatalogDataService, CatalogDataService>();
            
            MapperGlobal.Configure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager manager)
        {
            app.ConfigureExceptionHandler()
                .UseDeveloperExceptionPage()
                .UseSwagger()
                .UseSwaggerUI()
                .ServiceScope()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints()
                .UseHangfireDashboard("/hangfire");
            
            manager.AddHangfireRecurringJob();
        }
    }
}
