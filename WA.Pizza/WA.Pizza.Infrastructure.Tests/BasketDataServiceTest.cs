using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.Services;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;

namespace WA.Pizza.Infrastructure.Tests
{
    public class BasketDataServiceTest
    {
        [Fact]
        public async Task Succeed_return_all_existed_Baskets()
        {
            // Arrange
            ICollection<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.AddRange(baskets);
            await context.SaveChangesAsync();

            BasketDataService basketService = new BasketDataService(context);

            // Act
            BasketDto[] allBaskets = await basketService.GetAllBasketsAsync();
            
            // Assert
            allBaskets.Should().HaveCount(baskets.Count);
        }

        [Fact]
        public async Task Create_not_empty_basket_success()
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            var catalogItem = new CatalogItem { CatalogType = CatalogType.Pizza, Name = "Peperoni", Price = 25.1M, Quantity = 50, Description = "123"};
            context.CatalogItems.Add(catalogItem);
            await context.SaveChangesAsync();

            BasketDataService basketService = new BasketDataService(context);
            
            CreateBasketRequest createBasketRequest = new CreateBasketRequest
            {
                BasketItems = new List<BasketItemDto>
                {
                    new()
                    {
                        CatalogItemId = catalogItem.Id, Name = catalogItem.Name, Price = catalogItem.Price, Quantity = 2, Description = "123"
                    }
                }
            };

            // Act
            int basketId = await basketService.CreateBasketAsync(createBasketRequest);
            
            // Assert
            var basket = await context.Baskets.Include(i => i.BasketItems).FirstOrDefaultAsync(i => i.Id == basketId);
            basket.Should().NotBeNull();
            basket!.BasketItems.Should().NotBeEmpty();
            
            basket!.BasketItems.First().Should().BeEquivalentTo(createBasketRequest.BasketItems.First(),
                opt => opt.Excluding(i => i.BasketId));
        }

        [Fact]
        public async Task Successful_change_of_item_quantity()
        {
            // Arrange
            ICollection<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.AddRange(baskets);
            await context.SaveChangesAsync();

            int newQuantity = baskets.First().BasketItems.First().Quantity + 5;
            UpdateBasketRequest updateBasketRequest = new UpdateBasketRequest
            {
                Id = baskets.First().Id,
                BasketItems = new List<BasketItemDto>
                {
                    new()
                    {
                        Id = baskets.First().BasketItems.First().Id,
                        Quantity = newQuantity
                    }
                }
            };

            BasketDataService basketService = new BasketDataService(context);
            
            // Act
            int basketId = await basketService.UpdateBasketAsync(updateBasketRequest);

            // Assert
            var basket = await context.Baskets.Include(i => i.BasketItems).FirstOrDefaultAsync(i => i.Id == basketId);
            basket.Should().NotBeNull();
            basket!.BasketItems.Should().Contain(x => x.Quantity == newQuantity);
        }

        //[Fact]
        //public async Task Exception_will_returned_for_nonexistent_Id()
        //{
        //    // Arrange
        //    IEnumerable<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
        //    await using WAPizzaContext context = await DbContextFactory.CreateContext(baskets);
        //    BasketDataService basketService = new BasketDataService(context);
        //    UpdateBasketRequest basket = new UpdateBasketRequest();

        //    // Act
        //    Func<Task> func = async () => await basketService.UpdateBasketAsync(basket);

        //    // Assert
        //    await func.Should().ThrowAsync<ArgumentException>();
        //}

        //[Fact]
        //public async Task Exception_will_thrown_when_quantity_items_less_than_one()
        //{
        //    // Arrange
        //    Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
        //    await using WAPizzaContext context = await DbContextFactory.CreateContext(new []{ basket });
        //    BasketDataService basketService = new BasketDataService(context);
        //    UpdateBasketRequest basketRequest = new UpdateBasketRequest();

        //    // Act
        //    Func<Task> act = async () => await basketService.UpdateBasketAsync(basketRequest);

        //    // Assert
        //    await act.Should().ThrowAsync<ArgumentException>();
        //}

        //[Fact]
        //public async Task Successful_cleaning_of_basket_items()
        //{
        //    // Arrange
        //    Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
        //    await using WAPizzaContext context = await DbContextFactory.CreateContext(new []{ basket });
        //    BasketDataService basketService = new BasketDataService(context);

        //    // Act
        //    await basketService.CleanBasketItemsAsync(basket.Id);

        //    // Assert
        //    basket.BasketItems.Should().BeEmpty();
        //}

        //[Fact]
        //public async Task Successfully_return_error_with_non_existent_id()
        //{
        //    // Arrange
        //    Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
        //    await using WAPizzaContext context = await DbContextFactory.CreateContext(new[] { basket });
        //    BasketDataService basketService = new BasketDataService(context);
        //    int basketId = 5;

        //    // Act
        //    Func<Task> func = async () => await basketService.CleanBasketItemsAsync(basketId);

        //    // Assert
        //    await func.Should().ThrowAsync<ArgumentNullException>();
        //}

    }
}
