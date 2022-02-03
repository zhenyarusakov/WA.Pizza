using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.Services;
using WA.Pizza.Infrastructure.DTO.ClientDto;
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
        OperatorDataService operatorService = new OperatorDataService(context);
        CreateClientRequest createClientRequest = new()
        {
            Name = "pepsi",
            ApiToken = Guid.NewGuid(),
            WebSite = "pepsi.com"
        };

        // Act
        Guid newClient = await operatorService.CreateNewClientAsync(createClientRequest);

        // Assert
        Client client = await context.Clients.FirstOrDefaultAsync(x => x.ApiToken == newClient);
        client.Should().NotBeNull();
        // client!.ApiToken.Should().Be(createClientRequest.ApiToken);
        client!.Name.Should().Be(createClientRequest.Name);
        client!.WebSite.Should().Be(createClientRequest.WebSite);
    }

    [Fact]
    public async Task Remove_client_success()
    {
        // Arrange
        Client client = new Client()
        {
            Name = "pepsi",
            ApiToken = Guid.NewGuid(),
            WebSite = "pepsi.com"
        };
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        context.Clients.Add(client);
        await context.SaveChangesAsync();
        OperatorDataService operatorService = new OperatorDataService(context);
        int clientId = client.Id;
        
        // Act
        await operatorService.RemoveClientAsync(clientId);

        // Assert
        Client item = await context.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);
        item.Should().BeNull();
    }
}