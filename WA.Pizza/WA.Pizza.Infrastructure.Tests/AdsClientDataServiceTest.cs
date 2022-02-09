using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.Services.AdvertisementServices;
using WA.Pizza.Infrastructure.DTO.AdsClientDTO;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;
using Xunit;

namespace WA.Pizza.Infrastructure.Tests;

public class AdsClientDataServiceTest
{
    [Fact]
    public async Task Success_created_client_operator()
    {
        // Arrange
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        AdsClientDataService adsClientDataService = new AdsClientDataService(context);
        CreateAdsClientRequest createAdsClientRequest = new()
        {
            Name = "pepsi",
            WebSite = "pepsi.com"
        };

        // Act
        Guid newClient = await adsClientDataService.CreateNewAdsClientAsync(createAdsClientRequest);

        // Assert
        AdsClient adsClient = await context.AdsClients.FirstOrDefaultAsync(x => x.ApiKey == newClient);
        adsClient.Should().NotBeNull();
        adsClient!.Name.Should().Be(createAdsClientRequest.Name);
        adsClient!.WebSite.Should().Be(createAdsClientRequest.WebSite);
    }

    [Fact]
    public async Task Operator_success_deleted_client()
    {
        // Arrange
        AdsClient adsClient = new AdsClient()
        {
            Name = "pepsi",
            WebSite = "pepsi.com"
        };
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        context.AdsClients.Add(adsClient);
        await context.SaveChangesAsync();
        AdsClientDataService adsClientDataService = new AdsClientDataService(context);
        int clientId = adsClient.Id;
        
        // Act
        await adsClientDataService.RemoveAdsClientAsync(clientId);

        // Assert
        AdsClient item = await context.AdsClients.FirstOrDefaultAsync(x => x.Id == adsClient.Id);
        item.Should().BeNull();
    }

    [Fact]
    public async Task Operator_success_blocked_user()
    {
        // Arrange
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        AdsClientDataService adsClientDataService = new AdsClientDataService(context);
        CreateAdsClientRequest createAdsClientRequest = new()
        {
            Name = "pepsi",
            WebSite = "pepsi.com",
            IsBlocked = false
        };

        // Act
        Guid newClient = await adsClientDataService.CreateNewAdsClientAsync(createAdsClientRequest);
        
        // Assert
        AdsClient adsClient = await context.AdsClients.FirstOrDefaultAsync(x => x.ApiKey == newClient);
        adsClient!.IsBlocked.Should().Be(createAdsClientRequest.IsBlocked);
    }

    [Fact]
    public async Task Operator_has_success_unlocked_user()
    {
        // Arrange
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        AdsClientDataService adsClientDataService = new AdsClientDataService(context);
        CreateAdsClientRequest createAdsClientRequest = new()
        {
            Name = "pepsi",
            WebSite = "pepsi.com",
            IsBlocked = true
        };

        // Act
        Guid newClient = await adsClientDataService.CreateNewAdsClientAsync(createAdsClientRequest);
        
        // Assert
        AdsClient adsClient = await context.AdsClients.FirstOrDefaultAsync(x => x.ApiKey == newClient);
        adsClient!.IsBlocked.Should().Be(createAdsClientRequest.IsBlocked);
    }

    [Fact]
    public async Task Operator_must_success_change_client()
    {
        // Assert
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        AdsClient adsClient = new()
        {
            Name = "pepsi",
            ApiKey = Guid.NewGuid(),
            WebSite = "pepsi.com",
            IsBlocked = false,
            ClientInfo = "pepsi"
        };
        context.AdsClients.Add(adsClient);
        await context.SaveChangesAsync();
        AdsClientDataService adsClientDataService = new AdsClientDataService(context);

        UpdateAdsClientRequest updateAdsClientRequest = new()
        {
            Id = adsClient.Id,
            Name = adsClient.Name,
            ApiKey = adsClient.ApiKey,
            WebSite = adsClient.WebSite,
            ClientInfo = "pepsipepsi"
        };

        // Act
        int clientId = await adsClientDataService.UpdateAdsClientAsync(updateAdsClientRequest);

        // Assert
        AdsClient client = await context.AdsClients.FirstOrDefaultAsync(x => x.Id == clientId);
        client.Should().NotBeNull();
        client!.ClientInfo.Should().Be(updateAdsClientRequest.ClientInfo);
    }
}