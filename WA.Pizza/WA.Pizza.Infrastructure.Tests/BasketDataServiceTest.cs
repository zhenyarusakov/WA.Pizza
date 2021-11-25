using Xunit;
using System;
using System.Linq;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
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
            await using var context = await DbContextFactory.CreateAsync(BasketHelpers.GetAllBaskets());

            var basketService = new BasketDataService(context);

            // Act
            
            var baskets = await basketService.GetBasketsAsync();

            // Assert
            baskets.Should().NotBeNullOrEmpty();

            baskets.Select(x => x.Id).Should().Contain(2);
        }

        [Fact]
        public async Task Succeed_return_changed_name_for_BasketItem()
        {
            // Arrange
            await using var context = await DbContextFactory.CreateAsync(BasketHelpers.GetBasket());
            
            var basket = new UpdateBasketRequest(1, DateTime.Now,  1, new List<BasketItemDto>
            {
                new BasketItemDto
                {
                    Quantity = 3,
                    Description = "qwe",
                    Name = "qwe",
                    Price = 13
                }
            });

            var basketService = new BasketDataService(context);

            // Act
            var updateBasket = await basketService.UpdateBasketAsync(basket);

            // Assert
            updateBasket.Should().NotBeNull();
            updateBasket.Id.Should().Be(1);
            updateBasket.BasketItems.Select(x => x.Name)
                .Should().BeEquivalentTo("qwe");
        }

        [Fact]
        public async Task Exception_will_be_returned_the_absence_of_the_BasketItem_body()
        {
            // Arrange
            await using var context = await DbContextFactory.CreateAsync();
            var basketService = new BasketDataService(context);

            var basket = new UpdateBasketRequest(0, DateTime.Now, 1, new List<BasketItemDto>
            {
                new BasketItemDto
                {
                    Quantity = 1,
                    Description = "qwe",
                    Name = "qwe",
                    Price = 13
                }
            });

            basket.Id.Should().Be(0);
            // Act
            Func<Task> func = async () => await basketService.UpdateBasketAsync(basket);

            // Assert
            await func.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Exception_will_be_thrown_whenthe_cardinality_less_than_one()
        {
            // Arrange
            await using var context = await DbContextFactory.CreateAsync();
            var basketService = new BasketDataService(context);

            var basket = new UpdateBasketRequest(1, DateTime.Now, 1, new List<BasketItemDto>
            {
                new BasketItemDto
                {
                    Quantity = 0,
                    Description = "qwe",
                    Name = "qwe",
                    Price = 13
                }
            });

            basket.BasketItems.Select(x => x.Quantity < 1);
            // Act
            Func<Task> func = async () => await basketService.UpdateBasketAsync(basket);

            // Assert
            await func.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Successful_deletion_of_basket_items()
        {
            // Arrange
            await using var context = await DbContextFactory.CreateAsync(BasketHelpers.GetAllBaskets());
            var basketService = new BasketDataService(context);

            // Act
            var basket = basketService.DeleteBasketItemAsync(1);

            // Assert

            basket.Should().NotBeNull();
            basket.Id.Should().Be(1);
        }

        [Fact]
        public async Task Successfully_return_an_error_with_a_non_existent_identifier()
        {
            // Arrange
            await using var context = await DbContextFactory.CreateAsync(BasketHelpers.GetBasket());
            var basketService = new BasketDataService(context);

            // Act
            Func<Task> func = async () => await basketService.DeleteBasketItemAsync(9);

            // Assert

            await func.Should().ThrowAsync<ArgumentNullException>();
        }

    }
}
