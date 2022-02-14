using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.Queries;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;
using Xunit;

namespace WA.Pizza.Infrastructure.Tests.CQRS;

public class GetCatalogItemByIdQueryTest
{
    [Fact]
    public async Task Should_success_return_one_CatalogItem()
    {
        // Arrange 
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        List<CatalogItem> catalogs = new List<CatalogItem>();
        catalogs.Add(new CatalogItem
        {
            Id = 1,
            Name = "Desserts",
            Description = "Desserts",
            Price = 12,
            Quantity = 10,
            CatalogType = CatalogType.Desserts
        });
        catalogs.Add(new CatalogItem
        {
            Id = 2,
            Name = "Drinks",
            Description = "Drinks",
            Price = 12,
            Quantity = 15,
            CatalogType = CatalogType.Drinks
        });
        context.CatalogItems.AddRange(catalogs);
        await context.SaveChangesAsync();
        GetCatalogItemByIdQueryHandler queryHandler = new(context);
        GetByIdCatalogItemQuery request = new GetByIdCatalogItemQuery{Id = 1};

        // Act
        CatalogItemsListItem catalogItem = await queryHandler.Handle(request, new CancellationToken());

        // Assert
        CatalogItem? firstItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItem.Id);
        firstItem!.Id.Should().Be(catalogItem.Id);
        firstItem.Quantity.Should().Be(catalogItem.Quantity);
        firstItem.Name.Should().Be(catalogItem.Name);
        firstItem.Description.Should().Be(catalogItem.Description);
    }
}