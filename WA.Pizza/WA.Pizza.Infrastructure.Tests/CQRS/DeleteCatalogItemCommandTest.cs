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

public class DeleteCatalogItemCommandTest
{
    [Fact]
    public async Task One_CatalogItem_should_be_deleted_success()
    {
        // Arrange 
        await using WAPizzaContext context = await DbContextFactory.CreateContext();
        CatalogItem catalogItem = new()
        {
            Id = 1,
            Name = "Delete",
            Description = "Delete",
            Price = 11,
            Quantity = 11,
            CatalogBrandId = 1
        };
        context.CatalogItems.Add(catalogItem);
        await context.SaveChangesAsync();
        DeleteCatalogItemCommandHandler commandHandler = new(context);
        DeleteCatalogItemCommand request = new DeleteCatalogItemCommand{Id = 1};

        // Act
        await commandHandler.Handle(request, new CancellationToken());

        // Assert
        CatalogItem? item = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItem.Id);
        item!.Should().BeNull();
    }
}