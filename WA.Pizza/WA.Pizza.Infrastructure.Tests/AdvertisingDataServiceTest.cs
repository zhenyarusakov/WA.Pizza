using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.Services;
using WA.Pizza.Infrastructure.DTO.AdvertisingDTO;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;
using Xunit;

namespace WA.Pizza.Infrastructure.Tests;

public class AdvertisingDataServiceTest
{
    [Fact]
    public async Task Create_not_empty_Advertising()
    {
        // Arrange
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        CreateAdvertisingRequest createClientRequest = new()
        {
            Name = "pepsi",
            Description = "pepsi",
            Img = "pepsi.png",
            WebSite = "pepsi.com"
        };

        AdvertisingService advertisingService = new AdvertisingService(context);

        // Act
        int newAdvertising = await advertisingService.CreateAdvertisingAsync(createClientRequest);

        // Assert
        Advertising advertising = await context.Advertisings.FirstOrDefaultAsync(x => x.Id == newAdvertising);
        advertising.Should().NotBeNull();
        advertising!.Name.Should().Be(createClientRequest.Name);
        advertising!.Description.Should().Be(createClientRequest.Description);
        advertising!.Img.Should().Be(createClientRequest.Img);
        advertising!.WebSite.Should().Be(createClientRequest.WebSite);
    }

    [Fact]
    public async Task Return_all_Advertising_success()
    {
        // Arrange
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        ICollection<Advertising> advertisings = AdvertisingHelper.CreateListOfFilledAdvertising();
        context.Advertisings.AddRange(advertisings);
        await context.SaveChangesAsync();
        AdvertisingService advertisingService = new AdvertisingService(context);

        // Act
        AdvertisingDto[] getAllAdvertising = await advertisingService.GetAllAdvertisingAsync();

        // Assert
        Advertising[] contextAdvertisings = await context.Advertisings.ToArrayAsync();
        getAllAdvertising.Should().HaveCount(contextAdvertisings.Length);
        getAllAdvertising.Should().Equal(contextAdvertisings, (actual, expected) =>
            actual.Id == expected.Id &&
            actual.Name == expected.Name &&
            actual.Description == expected.Description &&
            actual.Img == expected.Img &&
            actual.WebSite == expected.WebSite
        );
    }

    [Fact]
    public async Task Return_one_Advertising_success()
    {
        // Arrange
        Advertising advertising = new Advertising()
        {
            Description = "pepsi",
            Img = "pepsi.png",
            Name = "pepsi",
            WebSite = "pepsi.com"
        };
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        context.Advertisings.AddRange(advertising);
        await context.SaveChangesAsync();
        AdvertisingService advertisingService = new AdvertisingService(context);

        // Act
        AdvertisingDto advertisingItem = await advertisingService.GetOneAdvertisingAsync(advertising.Id);
        
        // Assert
        Advertising firstItem = await context.Advertisings.FirstOrDefaultAsync(x => x.Id == advertisingItem.Id);
        firstItem!.Id.Should().Be(advertisingItem.Id);
        firstItem!.Name.Should().Be(advertisingItem.Name);
        firstItem!.Description.Should().Be(advertisingItem.Description);
        firstItem!.Img.Should().Be(advertisingItem.Img);
        firstItem!.WebSite.Should().Be(advertisingItem.WebSite);
    }

    [Fact]
    public async Task Advertising_data_changed_success()
    {
        // Assert
        Advertising advertising = new Advertising()
        {
            Description = "pepsi",
            Img = "pepsi.png",
            Name = "pepsi",
            WebSite = "pepsi.com"
        };
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        context.Advertisings.AddRange(advertising);
        await context.SaveChangesAsync();
        AdvertisingService advertisingService = new AdvertisingService(context);

        UpdateAdvertisingRequest updateAdvertisingRequest = new()
        {
            Id = advertising.Id,
            Description = "pepsipepsi",
            Img = "pepsi.png",
            Name = "pepsi",
            WebSite = "pepsi.com"
        };
        
        // Act
        int advertisingId = await advertisingService.UpdateAdvertisingAsync(updateAdvertisingRequest);
        
        // Assert
        Advertising firstItem = await context.Advertisings.FirstOrDefaultAsync(x => x.Id == advertisingId);
        firstItem.Should().NotBeNull();
        firstItem!.Description.Should().Be(updateAdvertisingRequest.Description);
    }

    [Fact]
    public async Task Delete_advertising_success()
    {
        // Assert
        Advertising advertising = new Advertising()
        {
            Description = "pepsi",
            Img = "pepsi.png",
            Name = "pepsi",
            WebSite = "pepsi.com"
        };
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        context.Advertisings.Add(advertising);
        await context.SaveChangesAsync();
        AdvertisingService advertisingService = new AdvertisingService(context);

        // Act
        await advertisingService.RemoveAdvertisingAsync(advertising.Id);
        
        // Assert
        Advertising advertisingItem = await context.Advertisings.FirstOrDefaultAsync(x => x.Id == advertising.Id);
        advertisingItem.Should().BeNull();
    }
        
}