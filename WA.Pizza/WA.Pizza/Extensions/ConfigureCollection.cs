using Microsoft.AspNetCore.Builder;

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
    }
}
