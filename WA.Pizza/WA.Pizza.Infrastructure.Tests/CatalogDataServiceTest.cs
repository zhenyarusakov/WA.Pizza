using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.Services;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;
using Xunit;

namespace WA.Pizza.Infrastructure.Tests
{
    public class CatalogDataServiceTest
    {
        [Fact]
        public async Task Successfully_return_one_existing_directory()
        {
            // Arrange
            ICollection<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService service = new CatalogDataService(context);
            int returnFirstId = catalogItems.First().Id;

            // Act
            CatalogItemDto catalogItemDto = await service.GetCatalogAsync(returnFirstId);

            // Assert
            catalogItemDto.Id.Should().Be(returnFirstId);
        }

        [Fact]
        public async Task Succeed_return_all_existed_CatalogItems()
        {
            // Arrange
            ICollection<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new CatalogDataService(context);

            // Act
            CatalogItemDto[] catalogItem = await catalogDataService.GetAllCatalogsAsync();

            // Assert
            catalogItem.Should().HaveCount(catalogItems.Count());
        }

        [Fact]
        public async Task Successful_creation_catalogItem()
        {
            // Arrange  
            ICollection<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new CatalogDataService(context);

            CreateCatalogRequest catalogRequest = new CreateCatalogRequest
            {
                Name = catalogItems.First().Name,
                Quantity = catalogItems.First().Quantity + 1,
                Description = catalogItems.First().Description,
                Price = catalogItems.First().Price
            };

            // Act
            int catalogItemId = await catalogDataService.CreateCatalogItemAsync(catalogRequest);

            // Assert
            CatalogItem catalogItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItemId);
            catalogItem.Should().NotBeNull();
            catalogItem!.Quantity.Should().Be(catalogRequest.Quantity);
        }

        [Fact]
        public async Task Successful_change_of_item_quantity_Catalog_Item()
        {
            // Arrange
            ICollection<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new CatalogDataService(context);

            int newQuantity = catalogItems.First().Quantity + 20;

            UpdateCatalogRequest catalogRequest = new UpdateCatalogRequest
            {
                Id = catalogItems.First().Id,
                Name = catalogItems.First().Name,
                Quantity = newQuantity,
                Description = catalogItems.First().Description,
                Price = catalogItems.First().Price
            };

            // Act
            int catalogItemId = await catalogDataService.UpdateCatalogItemAsync(catalogRequest);

            // Assert
            CatalogItem catalogItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItemId);
            catalogItem!.Quantity.Should().Be(catalogItem.Quantity);
            catalogItem.Should().NotBeNull();
        }

        [Fact]
        public async Task Exception_will_thrown_if_Id_CatalogItem_does_not_exist()
        {
            // Arrange
            ICollection<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new CatalogDataService(context);

            int newCatalogItemId = catalogItems.First().Id + 3;

            UpdateCatalogRequest catalogRequest = new UpdateCatalogRequest
            {
                Id = newCatalogItemId
            };

            // Act
            Func<Task> func = async () => await catalogDataService.UpdateCatalogItemAsync(catalogRequest);

            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Success_deleting_one_item_the_CatalogItem()
        {
            // Arrange
            ICollection<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new CatalogDataService(context);
            int catalogId = catalogItems.First().Id;

            // Act
            await catalogDataService.DeleteCatalogItemAsync(catalogId);

            CatalogItem result = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogId);

            // Assert
            result.Should().BeNull();
        }
    }
}
