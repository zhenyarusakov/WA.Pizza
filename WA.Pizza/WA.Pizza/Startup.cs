using Hangfire;
using MediatR;
using WA.Pizza.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using Twilio.Clients;
using WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;
using WA.Pizza.Infrastructure.Abstractions.SenderInterface;
using WA.Pizza.Infrastructure.Data.MapperConfiguration;
using WA.Pizza.Infrastructure.Data.Queries;
using WA.Pizza.Infrastructure.Data.Services.AdvertisementServices;
using WA.Pizza.Infrastructure.Data.Services.SenderServices;
using WA.Pizza.Infrastructure.DTO.MailSender;


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
                .AddScoped<IUserInfoProvider, AspNetUserInfoProvider>()
                .AddScoped<IMailService, MailService>()
                .AddScoped<ISmsSenderService, SmsSenderService>()
                .AddMediatR(typeof(GetAllCatalogItemQueryHandler).Assembly)
                .Configure<MailSettings>(Configuration.GetSection("MailSettings"))
                .AddIdentity()
                .AddAuthenticationOptions(Configuration)
                .AddHttpClient<ITwilioRestClient, TwilioClient>();

            MapperGlobal.Configure();
        }

        public void Configure(
            IApplicationBuilder app,
            IRecurringJobManager manager)
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
