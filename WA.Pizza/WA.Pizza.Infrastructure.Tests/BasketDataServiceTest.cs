using Xunit;
using System;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using WA.Pizza.Core.Entities.BasketDomain;
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
            IEnumerable<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(baskets);
            BasketDataService basketService = new BasketDataService(context);
            int basketId = 2;

            // Act
            BasketDto[] basketDtos = await basketService.GetAllBasketsAsync();
            
            // Assert
            basketDtos.Should().HaveCount(basketId);
        }

        [Fact]
        public async Task Successful_shopping_cart_creation()
        {
            // Arrange
            Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(new []{basket});
            BasketDataService basketService = new BasketDataService(context);

            CreateBasketRequest createBasketRequest = new CreateBasketRequest(new List<BasketItemDto>
            {
                new BasketItemDto
                {
                    Name = "",
                    Description = ""
                }
            });
            await context.SaveChangesAsync();

            // Act
            BasketDto basketDto = await basketService.CreateBasketAsync(createBasketRequest);
            
            // Assert
            basketDto.BasketItems.Should().NotBeNull();
        }

        [Fact]
        public async Task Successful_change_of_item_quantity()
        {
            // Arrange
            IEnumerable<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(baskets);

            UpdateBasketRequest updateBasketRequest = new UpdateBasketRequest
            {
                Id = 1,
                BasketItems = new List<BasketItemDto>
                {
                    new BasketItemDto
                    {
                        Quantity = 3,
                        Description = "qwe",
                        Name = "qwe",
                        Price = 12
                    }
                }
            };

            BasketDataService basketService = new BasketDataService(context);
            int quantity = 3;
            await context.SaveChangesAsync();

            // Act
            BasketDto updateBasket = await basketService.UpdateBasketAsync(updateBasketRequest);
            
            // Assert
            updateBasket.Should().NotBeNull();
            updateBasket.BasketItems.Should().Contain(x => x.Quantity == quantity);
        }

        [Fact]
        public async Task Exception_will_returned_for_nonexistent_Id()
        {
            // Arrange
            IEnumerable<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(baskets);
            BasketDataService basketService = new BasketDataService(context);
            UpdateBasketRequest basket = new UpdateBasketRequest();

            // Act
            Func<Task> func = async () => await basketService.UpdateBasketAsync(basket);

            // Assert
            await func.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Exception_will_thrown_when_quantity_items_less_than_one()
        {
            // Arrange
            Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(new []{ basket });
            BasketDataService basketService = new BasketDataService(context);
            UpdateBasketRequest basketRequest = new UpdateBasketRequest();

            // Act
            Func<Task> act = async () => await basketService.UpdateBasketAsync(basketRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Successful_cleaning_of_basket_items()
        {
            // Arrange
            Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(new []{ basket });
            BasketDataService basketService = new BasketDataService(context);
            await context.SaveChangesAsync();

            // Act
            await basketService.CleanBasketItemsAsync(basket.Id);

            // Assert
            basket.BasketItems.Should().BeEmpty();
        }

        [Fact]
        public async Task Successfully_return_error_with_non_existent_id()
        {
            // Arrange
            Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(new[] { basket });
            BasketDataService basketService = new BasketDataService(context);
            int basketId = 5;

            // Act
            Func<Task> func = async () => await basketService.CleanBasketItemsAsync(basketId);

            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

    }
}
