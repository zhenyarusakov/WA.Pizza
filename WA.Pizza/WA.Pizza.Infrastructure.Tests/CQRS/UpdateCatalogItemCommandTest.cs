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

public class UpdateCatalogItemCommandTest
{
    [Fact]
    public async Task CatalogItem_should_change_success()
    {
        // Arrange 
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        CatalogItem catalogItem = new()
        {
            Id = 1,
            Name = "Update",
            Description = "Update",
            Price = 12,
            Quantity = 12,
            CatalogBrandId = 1
        };
        context.CatalogItems.Add(catalogItem);
        await context.SaveChangesAsync();
        UpdateCatalogItemCommand command = new(context);
        UpdateCatalogRequest updateCatalog = new()
        {
            Id = 1,
            Name = "UpdateUpdate",
            Description = "UpdateUpdate",
            Price = 11,
            Quantity = 11,
            CatalogBrandId = 1
        };

        // Act
        int catalogItemId = await command.Handle(updateCatalog, new CancellationToken());

        // Assert
        CatalogItem item = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItemId);
        item.Should().NotBeNull();
        item!.Name.Should().Be(updateCatalog.Name);
        item!.Quantity.Should().Be(updateCatalog.Quantity);
        item!.Description.Should().Be(updateCatalog.Description);
        item!.Price.Should().Be(updateCatalog.Price);
    }
}