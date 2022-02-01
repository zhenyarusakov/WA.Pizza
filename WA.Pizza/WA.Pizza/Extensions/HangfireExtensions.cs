using Hangfire;
using WA.Pizza.Infrastructure.Data.Services;

namespace WA.Pizza.Api.Extensions;

public static class HangfireExtensions
{
    public static IRecurringJobManager AddHangfireRecurringJob(this IRecurringJobManager manager)
    {
        manager.AddOrUpdate<ForgottenBasketsJob>("Jobs", x => x.Run(), "0 */5 * * *");

        return manager;
    }
}