using Xunit;
using System.Linq;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using WA.Pizza.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Data.Services;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;
using WA.Pizza.Infrastructure.Tests.Customizations;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;

namespace WA.Pizza.Infrastructure.Tests
{
    public class CatalogDataServiceTest
    {
        [Fact]
        public async Task Return_one_existing_directory_success()
        {
            // Arrange
            ICollection<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService service = new (context);
            int returnFirstId = catalogItems.First().Id;

            // Act
            CatalogItemDto catalogItemDto = await service.GetCatalogAsync(returnFirstId);

            // Assert
            catalogItemDto.Id.Should().Be(returnFirstId);
        }

        [Fact]
        public async Task Return_all_existed_CatalogItems_success()
        {
            // Arrange
            ICollection<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new (context);

            // Act
            CatalogItemDto[] catalogItem = await catalogDataService.GetAllCatalogsAsync();

            // Assert
            catalogItem.Should().HaveCount(catalogItems.Count());
        }

        [Theory, CustomAutoData]
        public async Task Creation_catalogItem_success(CatalogItem catalogItems)
        {
            // Arrange  
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new (context);

            CreateCatalogRequest catalogRequest = new ()
            {
                Name = "Name",
                Quantity = catalogItems.Quantity
            };

            // Act
            int catalogItemId = await catalogDataService.CreateCatalogItemAsync(catalogRequest);

            // Assert
            CatalogItem catalogItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItemId);
            catalogItem.Should().NotBeNull();
            catalogItem!.Name.Should().Be(catalogRequest.Name);
            catalogItem!.Name.Should().NotBe(catalogItems.Name);
        }

        [Theory, CustomAutoData]
        public async Task Change_item_quantity_CatalogItem_success(CatalogItem catalogItems)
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new (context);

            int newQuantity = catalogItems.Quantity + 2;

            UpdateCatalogRequest catalogRequest = new ()
            {
                Id = catalogItems.Id,
                Name = catalogItems.Name,
                Quantity = newQuantity
            };

            // Act
            int catalogItemId = await catalogDataService.UpdateCatalogItemAsync(catalogRequest);

            // Assert
            CatalogItem catalogItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItemId);
            catalogItem.Should().NotBeNull();
            catalogItem!.Quantity.Should().Be(catalogItem.Quantity);
        }

        [Theory, CustomAutoData]
        public async Task Deleting_one_item_the_CatalogItem_success(CatalogItem catalogItems)
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new (context);
            int catalogId = catalogItems.Id;

            // Act
            await catalogDataService.DeleteCatalogItemAsync(catalogId);

            CatalogItem result = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogId);

            // Assert
            result.Should().BeNull();
        }
    }
}
