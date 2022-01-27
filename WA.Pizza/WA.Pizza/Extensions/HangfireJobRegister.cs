using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Hosting;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Api.Extensions;

public class HangfireJobRegister:  BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        RecurringJob.AddOrUpdate<IJobService>(x=>x.Run(), Cron.Hourly);
        return Task.CompletedTask;
        
        //Todu сделать крон раз в пять часов 
    }
}