using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers
{
    public static class DbContextExtensions
    {
        public static async Task<int> SaveChangesAndClearTracking(this DbContext context)
        {
            var result = await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            return result;
        }
    }
}
