using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WA.Pizza.Core.Entities;
using WA.Pizza.Core.Entities.IdentityModels;
using WA.Pizza.Infrastructure.Data;

namespace WA.Pizza.Api.Extensions
{
    public static class ConfigureCollection
    {
        public static IApplicationBuilder UseSwaggerUI( this IApplicationBuilder app)
        {
            return app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WA.Pizza v1"));
        }

        public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
        {
            return app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if(contextFeature != null)
                    {
                        await context.Response.WriteAsJsonAsync(new
                        {
                            Message = contextFeature.Error.Message,
                            Path = contextFeature.Path,
                            StackTrace = contextFeature.Error.StackTrace
                        });
                    }
                });
            });
        }

        public static IApplicationBuilder ServiceScope(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var appDbContext = serviceScope.ServiceProvider.GetRequiredService<WAPizzaContext>();
            appDbContext.Database.Migrate();

            return app;
        }

        public static IServiceProvider CreateAdminAccountAsync(this IServiceProvider app)
        {
            var services = app.CreateScope().ServiceProvider;

            var adminManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            CreateAdminAccount.CreateAdminAccountAsync(adminManager, roleManager);

            return app;
        }
    }
}
