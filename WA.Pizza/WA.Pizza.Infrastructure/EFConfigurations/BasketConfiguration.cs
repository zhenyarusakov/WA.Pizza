using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA.Pizza.Core.Entities.BasketDomain;

namespace WA.Pizza.Infrastructure.EFConfigurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder
                .HasMany(x => x.BasketItems)
                .WithOne(x => x.Basket)
                .HasForeignKey(x => x.BasketId);
        }
    }
}
