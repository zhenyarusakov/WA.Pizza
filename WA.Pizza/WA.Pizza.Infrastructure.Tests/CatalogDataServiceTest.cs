using Moq;
using Xunit;
using System.Linq;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoFixture;
using MediatR;
using WA.Pizza.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Data.Services;
using WA.Pizza.Infrastructure.Tests.Customizations;
using WA.Pizza.Infrastructure.Data.MapperConfiguration;
using WA.Pizza.Infrastructure.Data.ResponsibilitySegregation.CatalogItem.Commands;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;

namespace WA.Pizza.Infrastructure.Tests
{
    public class CatalogDataServiceTest
    {
        private readonly IMediator _mediator;
        public CatalogDataServiceTest(IMediator mediator)
        {
            _mediator = mediator;
            MapperGlobal.Configure();
        }

        [Theory, CustomAutoData]
        public async Task Return_one_CatalogItems_success(CatalogItem item)
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(item);
            await context.SaveChangesAsync();
            CatalogDataService service = new (new Mock<IMediator>().Object);
            int returnFirstId = item.Id;

            // Act
            CatalogItemDto catalogItem = await service.GetCatalogAsync(returnFirstId);

            // Assert
            CatalogItem firstItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItem.Id);
            firstItem!.Id.Should().Be(catalogItem.Id);
            firstItem!.Quantity.Should().Be(catalogItem.Quantity);
            firstItem!.Name.Should().Be(catalogItem.Name);
            firstItem!.Description.Should().Be(catalogItem.Description);
        }

        [Fact]
        public async Task Return_all_CatalogItems_success()
        {
            // Arrange
            ICollection<CatalogItem> catalogItems = CatalogHelper.CreateListOfFilledCatalogItems();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItems);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new (new Mock<IMediator>().Object);
            
            // Act
            CatalogItemDto[] getAllCatalogItems = await catalogDataService.GetAllCatalogsAsync();

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

        [Fact]
        public async Task Creation_catalogItem_success()
        {
            // Arrange  
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            CatalogDataService catalogDataService = new (new Mock<IMediator>().Object);

            CreateCatalogRequest catalogRequest = new ()
            {
                Name = "Pizza",
                Quantity = 1,
                Description = "Pizza",
                Price = 12
            };

            // Act
            int catalogItemId = await catalogDataService.CreateCatalogItemAsync(catalogRequest);

            // Assert
            CatalogItem catalogItem = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItemId);
            catalogItem.Should().NotBeNull();
            catalogItem!.Name.Should().Be(catalogRequest.Name);
            catalogItem!.Quantity.Should().Be(catalogRequest.Quantity);
            catalogItem!.Description.Should().Be(catalogRequest.Description);
            catalogItem!.Price.Should().Be(catalogRequest.Price);
        }

        [Theory, CustomAutoData]
        public async Task Change_CatalogItem_success(CatalogItem item)
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(item);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new (new Mock<IMediator>().Object);
            
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
            catalogItem!.Name.Should().Be(catalogRequest.Name);
            catalogItem!.Quantity.Should().Be(catalogRequest.Quantity);
            catalogItem!.Description.Should().Be(catalogRequest.Description);
            catalogItem!.Price.Should().Be(catalogRequest.Price);
        }

        [Theory, CustomAutoData]
        public async Task Deleting_catalogItem_success(CatalogItem catalogItem)
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItem);
            await context.SaveChangesAsync();
            CatalogDataService catalogDataService = new (new Mock<IMediator>().Object);
            int catalogId = catalogItem.Id;

            // Act
            await catalogDataService.DeleteCatalogItemAsync(catalogId);

            // Assert
            CatalogItem item = await context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItem.Id);
            item!.Should().BeNull();
        }
    }
}
