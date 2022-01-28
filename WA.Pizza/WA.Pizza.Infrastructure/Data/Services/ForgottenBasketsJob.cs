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
        var now = DateTime.UtcNow;

        var baskets = await _context.Baskets
            .Where(x=> x.LastModified <= now.AddHours(-1) && x.User != null)
            .ToArrayAsync(); 

        foreach (var basket in baskets)
        {
            _logger.LogInformation($"{basket.LastModified} Hey friend! You have a range of products in your shopping cart. Don't want to continue shopping?");
        }
    }
}
