using System.Collections.Generic;
using System.Threading.Tasks;
using WA.Pizza.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WA.Pizza.Core.Entities;

namespace WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers
{
    public static class DbContextFactory
    {
        private const string DbName = "WA.Pizza";

        public static async Task<WAPizzaContext> CreateContext()
        {
            DbContextOptions<WAPizzaContext> builder = new DbContextOptionsBuilder<WAPizzaContext>()
                .UseInMemoryDatabase(DbName)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            WAPizzaContext context = new WAPizzaContext(builder);
            return context;
        }
    }
}
