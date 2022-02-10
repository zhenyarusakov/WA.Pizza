using System;
using Hangfire;
using MediatR;
using WA.Pizza.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;
using WA.Pizza.Infrastructure.Data.MapperConfiguration;
using WA.Pizza.Infrastructure.Data.ResponsibilitySegregation.CatalogItem.Queries;
using WA.Pizza.Infrastructure.Data.Services.AdvertisementServices;


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
                .AddDbContextIdentity(Configuration)
                .AddControllersOptions()
                .AddHangfireServer()
                .AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnectionDb")))
                .AddScoped<IOrderDataService, OrderDataService>()
                .AddScoped<IBasketDataService, BasketDataService>()
                .AddScoped<IAuthenticateService, AuthenticateService>()
                .AddScoped<IAdsClientDataService, AdsClientDataService>()
                .AddScoped<IAdvertisementDataService, AdvertisementDataService>()
                .AddMediatR(typeof(GetAllCatalogItemQuery).Assembly)
                .AddIdentity()
                .AddAuthenticationOptions(Configuration);
            
            services.AddMediatR(typeof(GetAllCatalogItemQuery).Assembly);
            MapperGlobal.Configure();
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            IRecurringJobManager manager, 
            IServiceProvider serviceProvider)
        {
            app.ConfigureExceptionHandler()
                .UseDeveloperExceptionPage()
                .UseSwagger()
                .UseSwaggerUI()
                .ServiceScope()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints()
                .UseHangfireDashboard("/hangfire");

            manager.AddHangfireRecurringJob();
        }
    }
}
