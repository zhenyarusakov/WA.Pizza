using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.Services;
using WA.Pizza.Infrastructure.DTO.AdsClientDTO;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;
using Xunit;

namespace WA.Pizza.Infrastructure.Tests;

public class OperatorDataServiceTest
{
    [Fact]
    public async Task Create_not_empty_Client()
    {
        // Arrange
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        OperatorDataDataService operatorDataService = new OperatorDataDataService(context);
        CreateAdsClientRequest createAdsClientRequest = new()
        {
            Name = "pepsi",
            WebSite = "pepsi.com"
        };

        // Act
        Guid newClient = await operatorDataService.CreateNewAdsClientAsync(createAdsClientRequest);

        // Assert
        AdsClient adsClient = await context.AdsClients.FirstOrDefaultAsync(x => x.ApiKey == newClient);
        adsClient.Should().NotBeNull();
        adsClient!.Name.Should().Be(createAdsClientRequest.Name);
        adsClient!.WebSite.Should().Be(createAdsClientRequest.WebSite);
    }

    [Fact]
    public async Task Remove_client_success()
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
        OperatorDataDataService operatorDataService = new OperatorDataDataService(context);
        int clientId = adsClient.Id;
        
        // Act
        await operatorDataService.RemoveAdsClientAsync(clientId);

        // Assert
        AdsClient item = await context.AdsClients.FirstOrDefaultAsync(x => x.Id == adsClient.Id);
        item.Should().BeNull();
    }
}