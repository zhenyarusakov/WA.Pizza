using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Infrastructure.Data.Services;

public class ForgottenBasketsJob: IJobService
{
    private readonly WAPizzaContext _context;
    private readonly ILogger<ForgottenBasketsJob> _logger;

    public ForgottenBasketsJob(WAPizzaContext context, ILogger<ForgottenBasketsJob> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task Run()
    {
        var baskets = await _context.Baskets.ToArrayAsync();
        var now = DateTimeOffset.UtcNow;
        
        var lowerBound = now.AddHours(1);
        var upperBound = now.AddDays(1);

        foreach (var basket in baskets)
        {
            if (now.IsInRange(lowerBound, upperBound))
            {
                _logger.LogInformation($"Baskets hourly check completed successfully {basket.Id}");
            }
        }
    }
}

public static class DateTimeExtensions
{
    public static bool IsInRange(this DateTimeOffset dateTimeOffset, DateTimeOffset start, DateTimeOffset end)
    {
        return dateTimeOffset >= start && dateTimeOffset < end;
    }
}