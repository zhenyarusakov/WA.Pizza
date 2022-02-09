using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.Services.AdvertisementServices;
using WA.Pizza.Infrastructure.DTO.AdvertisementDTO;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;
using Xunit;

namespace WA.Pizza.Infrastructure.Tests;

public class AdvertisingDataServiceTest
{
    [Fact]
    public async Task Client_success_receives_the_advertisement()
    {
        // Arrange
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        AdsClient client = new AdsClient
        {
            Id = 1,
            Name = "pepsi",
            ApiKey = Guid.NewGuid(),
            WebSite = "pepsi",
            IsBlocked = true,
        };
        context.AdsClients.Add(client);
        await context.SaveChangesAsync();
        
        CreateAdvertisementRequest createClientRequest = new()
        {
            Name = "pepsi",
            Description = "pepsi",
            Img = "pepsi.png",
            WebSite = "pepsi.com",
            AdsClientId = 1
        };
        AdvertisementDataService advertisementDataService = new AdvertisementDataService(context);

        // Act
        int newAdvertising = await advertisementDataService.CreateAdvertisementAsync(createClientRequest);

        // Assert
        Advertisement advertisement = await context.Advertisements.FirstOrDefaultAsync(x => x.Id == newAdvertising);
        advertisement.Should().NotBeNull();
        advertisement!.Name.Should().Be(createClientRequest.Name);
        advertisement!.Description.Should().Be(createClientRequest.Description);
        advertisement!.Img.Should().Be(createClientRequest.Img);
        advertisement!.WebSite.Should().Be(createClientRequest.WebSite);
    }

    [Fact]
    public async Task Client_success_receives_all_advertisements()
    {
        // Arrange
        AdsClient client = new AdsClient
        {
            Id = 1,
            Name = "pepsi",
            ApiKey = Guid.NewGuid(),
            WebSite = "pepsi",
            IsBlocked = true,
        };
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        ICollection<Advertisement> advertisings = AdvertisementHelper.CreateListOfFilledAdvertising();
        context.Advertisements.AddRange(advertisings);
        context.AdsClients.AddRange(client);
        await context.SaveChangesAsync();
        AdvertisementDataService advertisementDataService = new AdvertisementDataService(context);

        // Act
        AdvertisementDto[] getAllAdvertising = await advertisementDataService.GetAllAdvertisementAsync(client.ApiKey);

        // Assert
        Advertisement[] contextAdvertisings = await context.Advertisements.ToArrayAsync();
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
    public async Task Client_success_receives_one_advertisement()
    {
        // Arrange
        AdsClient client = new AdsClient
        {
            Id = 1,
            Name = "pepsi",
            ApiKey = Guid.NewGuid(),
            WebSite = "pepsi",
            IsBlocked = true,
        };
        Advertisement advertisement = new Advertisement()
        {
            Description = "pepsi",
            Img = "pepsi.png",
            Name = "pepsi",
            WebSite = "pepsi.com",
            AdsClientId = 1
        };
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        context.Advertisements.AddRange(advertisement);
        context.AdsClients.AddRange(client);
        await context.SaveChangesAsync();
        AdvertisementDataService advertisementDataService = new AdvertisementDataService(context);

        // Act
        AdvertisementDto advertisementItem = await advertisementDataService.GetOneAdvertisementAsync(advertisement.Id);
        
        // Assert
        Advertisement firstItem = await context.Advertisements.FirstOrDefaultAsync(x => x.Id == advertisementItem.Id);
        firstItem!.Id.Should().Be(advertisementItem.Id);
        firstItem!.Name.Should().Be(advertisementItem.Name);
        firstItem!.Description.Should().Be(advertisementItem.Description);
        firstItem!.Img.Should().Be(advertisementItem.Img);
        firstItem!.WebSite.Should().Be(advertisementItem.WebSite);
    }

    [Fact]
    public async Task Client_success_changed_the_advertisement()
    {
        // Assert
        AdsClient client = new AdsClient
        {
            Id = 1,
            Name = "pepsi",
            ApiKey = Guid.NewGuid(),
            WebSite = "pepsi",
            IsBlocked = true,
        };
        Advertisement advertisement = new Advertisement()
        {
            Description = "pepsi",
            Img = "pepsi.png",
            Name = "pepsi",
            WebSite = "pepsi.com",
            AdsClientId = 1
        };
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        context.Advertisements.AddRange(advertisement);
        context.AdsClients.AddRange(client);
        await context.SaveChangesAsync();
        AdvertisementDataService advertisementDataService = new AdvertisementDataService(context);

        UpdateAdvertisementRequest updateAdvertisementRequest = new()
        {
            Id = advertisement.Id,
            Description = "pepsipepsi",
            Img = "pepsi.png",
            Name = "pepsi",
            WebSite = "pepsi.com"
        };
        
        // Act
        int advertisingId = await advertisementDataService.UpdateAdvertisementAsync(updateAdvertisementRequest);
        
        // Assert
        Advertisement firstItem = await context.Advertisements.FirstOrDefaultAsync(x => x.Id == advertisingId);
        firstItem.Should().NotBeNull();
        firstItem!.Description.Should().Be(updateAdvertisementRequest.Description);
    }

    [Fact]
    public async Task Client_success_removed_advertisement()
    {
        // Assert
        AdsClient client = new AdsClient
        {
            Id = 1,
            Name = "pepsi",
            ApiKey = Guid.NewGuid(),
            WebSite = "pepsi",
            IsBlocked = true,
        };
        Advertisement advertisement = new Advertisement()
        {
            Description = "pepsi",
            Img = "pepsi.png",
            Name = "pepsi",
            WebSite = "pepsi.com",
            AdsClientId = 1
        };
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        context.Advertisements.Add(advertisement);
        context.AdsClients.Add(client);
        await context.SaveChangesAsync();
        AdvertisementDataService advertisementDataService = new AdvertisementDataService(context);

        // Act
        await advertisementDataService.RemoveAdvertisementAsync(advertisement.Id);
        
        // Assert
        Advertisement advertisementItem = await context.Advertisements.FirstOrDefaultAsync(x => x.Id == advertisement.Id);
        advertisementItem.Should().BeNull();
    }
    
    [Fact]
    public async Task Blocked_user_should_not_be_able_to_access_functionality()
    {
        // Arrange
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        AdsClient clientRequest = new()
        {
            Id = 1,
            Name = "pepsi",
            ApiKey = Guid.NewGuid(),
            WebSite = "pepsi",
            IsBlocked = false
        };
        Advertisement advertisementPepsi = new()
        {
            Id = 1,
            Name = "pepsi",
            Description = "pepsi",
            Img = "pepsi",
            WebSite = "pepsi",
            AdsClientId = 1
        };
        context.AdsClients.Add(clientRequest);
        context.Advertisements.Add(advertisementPepsi);
        await context.SaveChangesAsync();

        AdvertisementDataService advertisementDataService = new AdvertisementDataService(context);

        // Act
        AdvertisementDto[] newAdvertising = await advertisementDataService.GetAllAdvertisementAsync(clientRequest.ApiKey);

        // Assert
        newAdvertising.Should().BeEmpty();
    }
}