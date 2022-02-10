using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.ResponsibilitySegregation.CatalogItem.Queries;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;
using Xunit;

namespace WA.Pizza.Infrastructure.Tests.CQRS;

public class GetAllCatalogItemQueryTest
{
    [Fact]
    public async Task Should_success_return_all_CatalogItem()
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
        GetAllCatalogItemQuery query = new(context);
        CatalogItemDto request = new CatalogItemDto();

        // Act
        CatalogItemDto[] getAllCatalogItems = await query.Handle(request, new CancellationToken());

        // Assert
        CatalogItem[] contextCatalogItems = await context.CatalogItems.ToArrayAsync();
        getAllCatalogItems.Should().HaveCount(contextCatalogItems.Count());
        getAllCatalogItems.Should().Equal(contextCatalogItems, (actual, expected) =>
            actual.Id == expected.Id &&
            actual.Quantity == expected.Quantity &&
            actual.Price == expected.Price &&
            actual.Description == expected.Description &&
            actual.Name == expected.Name);
    }
}