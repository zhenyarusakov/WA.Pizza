using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using WA.Pizza.Infrastructure.Data.Services;

namespace WA.Pizza.Api.Extensions;

public static class HangfireExtensions
{
    public static IServiceCollection AddHangfireRecurringJob(this IServiceCollection services)
    {
        RecurringJob.AddOrUpdate<ForgottenBasketsJob>(x=>x.Run(), Cron.Minutely);

        return services;
    }
}