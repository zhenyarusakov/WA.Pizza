using Xunit;
using System.Linq;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using WA.Pizza.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Data.Services;
using WA.Pizza.Infrastructure.Tests.Customizations;
using WA.Pizza.Infrastructure.Data.MapperConfiguration;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;

namespace WA.Pizza.Infrastructure.Tests
{
    public class CatalogDataServiceTest
    {
        public CatalogDataServiceTest()
        {
            MapperGlobal.Configure();
        }

        [Theory, CustomAutoData]
        public async Task Return_one_existing_directory_success(CatalogItem item)
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(item);
            await context.SaveChangesAsync();
            CatalogDataService service = new (context);
            int returnFirstId = item.Id;

            // Act
            CatalogItemDto catalogItem = await service.GetCatalogAsync(returnFirstId);

            // Assert
            CatalogItem firstItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItem.Id);
            firstItem!.Id.Should().Be(catalogItem.Id);
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
            int catalogItemsCount = context.CatalogItems.Count();
            catalogItemsCount!.Should().Be(catalogItems.Count);
        }

        [Fact]
        public async Task Creation_catalogItem_success()
        {
            // Arrange  
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            CatalogDataService catalogDataService = new (context);

            CreateCatalogRequest catalogRequest = new ()
            {
                Name = "Name",
                Quantity = 1
            };

            // Act
            int catalogItemId = await catalogDataService.CreateCatalogItemAsync(catalogRequest);

            // Assert
            CatalogItem catalogItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItemId);
            catalogItem.Should().NotBeNull();
            catalogItem!.Name.Should().Be(catalogRequest.Name);
            catalogItem!.Quantity.Should().Be(catalogRequest.Quantity);
        }

        [Theory, CustomAutoData]
        public async Task Change_item_quantity_CatalogItem_success(CatalogItem item)
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(item);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new (context);
            
            UpdateCatalogRequest catalogRequest = new ()
            {
                Id = item.Id,
                Name = "Name",
                Quantity = 3,
                Description = "Description",
                Price = 12
            };

            // Act
            int catalogItemId = await catalogDataService.UpdateCatalogItemAsync(catalogRequest);

            // Assert
            CatalogItem catalogItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItemId);
            catalogItem.Should().NotBeNull();
            catalogItem!.Name.Should().Be(catalogItem.Name);
            catalogItem!.Quantity.Should().Be(catalogItem.Quantity);
            catalogItem!.Description.Should().Be(catalogItem.Description);
            catalogItem!.Price.Should().Be(catalogItem.Price);
        }

        [Theory, CustomAutoData]
        public async Task Deleting_catalogItem_success(CatalogItem catalogItem)
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItem);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new (context);
            int catalogId = catalogItem.Id;

            // Act
            await catalogDataService.DeleteCatalogItemAsync(catalogId);

            // Assert
            CatalogItem item = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItem.Id);
            item!.Should().BeNull();
        }
    }
}
