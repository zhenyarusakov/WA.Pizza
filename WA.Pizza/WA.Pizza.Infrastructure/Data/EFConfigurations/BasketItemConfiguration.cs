using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA.Pizza.Core.Entities.BasketDomain;

namespace WA.Pizza.Infrastructure.Data.EFConfigurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(x => x.Price)
                .HasColumnType("decimal(20,8)");

            builder.HasOne(x => x.CatalogItem).WithMany();
        }
    }
}
