using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.Data.EFConfigurations
{
    public class CatalogItemConfiguration : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
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
        }
    }
}
