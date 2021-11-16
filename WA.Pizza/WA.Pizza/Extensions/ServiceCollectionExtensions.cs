using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WA.Pizza.Infrastructure.Data;

namespace WA.Pizza.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServicesExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WA.Pizza", Version = "v1" });
            });
        }

        public static void AddDbContextExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WAPizzaContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddControllersWithViewsExtensions(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions
                    .ReferenceHandler = ReferenceHandler.Preserve);
        }
    }
}
