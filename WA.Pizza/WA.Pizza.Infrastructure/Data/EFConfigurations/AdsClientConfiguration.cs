﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA.Pizza.Core.Entities;

namespace WA.Pizza.Infrastructure.Data.EFConfigurations;

public class AdsClientConfiguration : IEntityTypeConfiguration<AdsClient>
{
    public void Configure(EntityTypeBuilder<AdsClient> builder)
    {
        builder
            .HasMany(x => x.Advertisings)
            .WithOne(x => x.AdsClient)
            .HasForeignKey(x => x.AdsClientId);
    }
}