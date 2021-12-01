//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using WA.Pizza.Core.Entities.BasketDomain;
//using WA.Pizza.Core.Entities.CatalogDomain;
//using WA.Pizza.Infrastructure.Data;
//using WA.Pizza.Infrastructure.Data.Services;
//using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;
//using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;
//using Xunit;

//namespace WA.Pizza.Infrastructure.Tests
//{
//    public class CatalogDataServiceTest
//    {
//        [Fact]
//        public async Task Successfully_return_one_existing_directory()
//        {
//            // Arrange
//            IEnumerable<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
//            await using WAPizzaContext context = await DbContextFactory.CreateContext(catalogItems);
//            CatalogDataService service = new CatalogDataService(context);
//            int catalogId = 1;

//            // Act
//            CatalogItemDto catalogItemDto = await service.GetCatalogAsync(catalogId);

//            // Assert
//            catalogItemDto.Id.Should().Be(catalogId);
//        }

//        [Fact]
//        public async Task Exception_will_return_for_non_existent_CatalogItem_id()
//        {
//            // Arrange
//            IEnumerable<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
//            await using WAPizzaContext context = await DbContextFactory.CreateContext(catalogItems);
//            CatalogDataService catalogDataService = new CatalogDataService(context);
//            int catalogId = 5;

//            // Act
//            Func<Task> func = async () => await catalogDataService.GetCatalogAsync(catalogId);

//            // Assert
//            await func.Should().ThrowAsync<ArgumentNullException>();
//        }

//        [Fact]
//        public async Task Succeed_return_all_existed_CatalogItems()
//        {
//            // Arrange
//            IEnumerable<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
//            await using WAPizzaContext context = await DbContextFactory.CreateContext(catalogItems);
//            CatalogDataService catalogDataService = new CatalogDataService(context);
//            int catalogId = 2;

//            // Act
//            CatalogItemDto[] catalogItem = await catalogDataService.GetAllCatalogsAsync();

//            // Assert
//            catalogItem.Should().HaveCount(catalogId);
//        }

//        [Fact]
//        public async Task Successful_creation_of_the_catalogItem()
//        {
//            // Arrange
//            IEnumerable<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
//            await using WAPizzaContext context = await DbContextFactory.CreateContext(catalogItems);
//            CatalogDataService catalogDataService = new CatalogDataService(context);

//            CreateCatalogRequest catalogRequest = new CreateCatalogRequest
//            {
//                Name = "qwe",
//                Quantity = 10,
//                Description = "qwe",
//                Price = 12,
//                CatalogBrandId = 1,
//                BasketItems = new List<BasketItem>
//                {
//                    new ()
//                    {
//                        Name = "qwe",
//                        Quantity = 1,
//                        Price = 12,
//                        Description = "qwe"
//                    }
//                }
//            };
//            await context.SaveChangesAsync();

//            // Act
//            CatalogItemDto catalogItemDto = await catalogDataService.CreateCatalogItemAsync(catalogRequest);

//            // Assert
//            catalogItemDto.Should().NotBeNull();
//            catalogItemDto.Name.Should().Be("qwe");
//        }

//        [Fact]
//        public async Task Successful_change_of_item_quantity_Catalog_Item()
//        {
//            // Arrange
//            IEnumerable<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
//            await using WAPizzaContext context = await DbContextFactory.CreateContext(catalogItems);
//            CatalogDataService catalogDataService = new CatalogDataService(context);

//            UpdateCatalogRequest catalogRequest = new UpdateCatalogRequest
//            {
//                Id = 1,
//                Name = "BasketItem",
//                Quantity = 11,
//                Description = "BasketItem",
//                Price = 12,
//                CatalogBrandId = 1
//            };
//            await context.SaveChangesAsync();

//            // Act
//            CatalogItemDto catalogItemDto = await catalogDataService.UpdateCatalogItemAsync(catalogRequest);

//            // Assert
//            catalogItemDto.Should().NotBeNull();
//            catalogItemDto.Name.Should().Contain("BasketItem");
//            catalogItemDto.Quantity.Should().NotBe(10);
//        }

//        [Fact]
//        public async Task Exception_will_thrown_if_Id_CatalogItem_does_not_exist()
//        {
//            // Arrange
//            IEnumerable<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
//            await using WAPizzaContext context = await DbContextFactory.CreateContext(catalogItems);
//            CatalogDataService catalogDataService = new CatalogDataService(context);
//            UpdateCatalogRequest catalogRequest = new UpdateCatalogRequest
//            {
//                Id = 3
//            };
//            await context.SaveChangesAsync();

//            // Act
//            Func<Task> func = async () => await catalogDataService.UpdateCatalogItemAsync(catalogRequest);

//            // Assert
//            await func.Should().ThrowAsync<ArgumentNullException>();
//        }

//        [Fact]
//        public async Task Success_deleting_one_item_the_CatalogItem()
//        {
//            // Arrange
//            IEnumerable<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
//            await using WAPizzaContext context = await DbContextFactory.CreateContext(catalogItems);
//            CatalogDataService catalogDataService = new CatalogDataService(context);
//            int catalogId = 1;
//            await context.SaveChangesAsync();

//            // Act
//            await catalogDataService.DeleteCatalogItemAsync(catalogId);

//            CatalogItem result =  await context.CatalogItems.SingleOrDefaultAsync(x => x.Id == catalogId);

//            // Assert
//            result.Should().BeNull();
//        }

//        [Fact]
//        public async Task Exception_will_thrown_when_deleting_non_existent_Id()
//        {
//            // Arrange
//            IEnumerable<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
//            await using WAPizzaContext context = await DbContextFactory.CreateContext(catalogItems);
//            CatalogDataService catalogDataService = new CatalogDataService(context);
//            UpdateCatalogRequest catalogRequest = new UpdateCatalogRequest
//            {
//                Id = 3
//            };
//            await context.SaveChangesAsync();

//            // Act
//            Func<Task> func = async () => await catalogDataService.UpdateCatalogItemAsync(catalogRequest);

//            // Assert
//            await func.Should().ThrowAsync<ArgumentNullException>();
//        }
//    }
//}
