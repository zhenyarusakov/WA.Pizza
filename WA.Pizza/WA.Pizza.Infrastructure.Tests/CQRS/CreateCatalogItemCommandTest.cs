using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.ResponsibilitySegregation.CatalogItem.Commands;
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
        CreateCatalogRequest catalogRequest = new()
        {
            Name = "Create",
            Description = "Create",
            Price = 11,
            Quantity = 11,
            CatalogBrandId = 1
        };
        CreateCatalogItemCommand command = new(context);
        
        // Act
        int catalogItemId = await command.Handle(catalogRequest, new CancellationToken());

        // Assert
        CatalogItem catalogItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItemId);
        catalogItem.Should().NotBeNull();
        catalogItem!.Name.Should().Be(catalogRequest.Name);
        catalogItem!.Description.Should().Be(catalogRequest.Description);
        catalogItem!.Price.Should().Be(catalogRequest.Price);
        catalogItem!.Quantity.Should().Be(catalogRequest.Quantity);
        catalogItem!.CatalogBrandId.Should().Be(catalogRequest.CatalogBrandId);
    }
}