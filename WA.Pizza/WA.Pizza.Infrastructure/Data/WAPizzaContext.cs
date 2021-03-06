using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Core.Entities.IdentityModels;
using WA.Pizza.Core.Entities.OrderDomain;

namespace WA.Pizza.Infrastructure.Data
{
    public class WAPizzaContext: IdentityDbContext<ApplicationUser>
    {
        public WAPizzaContext(DbContextOptions<WAPizzaContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WAPizzaContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Basket> Baskets { get; set; } 
        public DbSet<BasketItem> BasketItems { get; set; } 
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<AdsClient> AdsClients { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<TokenModel> TokenModels { get; set; }

    }
}
