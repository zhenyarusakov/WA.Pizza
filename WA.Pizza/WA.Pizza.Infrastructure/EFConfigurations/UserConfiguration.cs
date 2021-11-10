using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA.Pizza.Core.Entities;

namespace WA.Pizza.Infrastructure.EFConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Age)
                .IsRequired();

            builder
                .Property(x => x.City)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.HouseNumber)
                .IsRequired();

            builder
                .Property(x => x.EntranceNumber)
                .IsRequired();

            builder
                .Property(x => x.ApartmentNumber)
                .IsRequired();

            builder
                .HasMany(x => x.Orders)
                .WithOne(x => x.User);
        }
    }
}
