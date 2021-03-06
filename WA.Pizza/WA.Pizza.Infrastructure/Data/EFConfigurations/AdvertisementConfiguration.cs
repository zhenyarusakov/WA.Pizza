using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA.Pizza.Core.Entities;

namespace WA.Pizza.Infrastructure.Data.EFConfigurations;

public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder
            .Property(x => x.Name)
            .HasMaxLength(128)
            .IsRequired();
        
        builder
            .Property(x => x.WebSite)
            .HasMaxLength(256)
            .IsRequired();
        
        builder
            .Property(x => x.Img)
            .HasMaxLength(256)
            .IsRequired();
    }
}