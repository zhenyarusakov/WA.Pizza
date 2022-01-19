using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace WA.Pizza.Api.Extensions
{
    public static class ConfigureCollection
    {
        public static void UseSwaggerUI( this IApplicationBuilder app)
        {
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WA.Pizza v1"));
        }

        public static void UseEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
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
    }
}
