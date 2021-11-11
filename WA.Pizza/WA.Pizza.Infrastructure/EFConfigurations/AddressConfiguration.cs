using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA.Pizza.Core.Entities.OrderDomain;

namespace WA.Pizza.Infrastructure.EFConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder
                .Property(x => x.Country)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.City)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Street)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.House)
                .IsRequired();

            builder
                .Property(x => x.Entrance)
                .IsRequired();

            builder
                .Property(x => x.ApartmentNumber)
                .IsRequired();
        }
    }
}