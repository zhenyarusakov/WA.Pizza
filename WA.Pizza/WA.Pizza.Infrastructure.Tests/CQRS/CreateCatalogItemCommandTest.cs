using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;
using Xunit;

namespace WA.Pizza.Infrastructure.Tests.CQRS;

public class CreateCatalogItemCommandTest
{
    [Fact]
    public async Task CatalogItem_should_be_created_success()
    {
        // Arrange 
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        CreateCatalogItemCommand catalogItemCommand = new()
        {
            Name = "Create",
            Description = "Create",
            Price = 11,
            Quantity = 11,
            CatalogBrandId = 1
        };
        CreateCatalogItemCommandHandler commandHandler = new(context);
        
        // Act
        int catalogItemId = await commandHandler.Handle(catalogItemCommand, new CancellationToken());

        // Assert
        CatalogItem? catalogItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItemId);
        catalogItem.Should().NotBeNull();
        catalogItem!.Name.Should().Be(catalogItemCommand.Name);
        catalogItem.Description.Should().Be(catalogItemCommand.Description);
        catalogItem.Price.Should().Be(catalogItemCommand.Price);
        catalogItem.Quantity.Should().Be(catalogItemCommand.Quantity);
        catalogItem.CatalogBrandId.Should().Be(catalogItemCommand.CatalogBrandId);
    }
}