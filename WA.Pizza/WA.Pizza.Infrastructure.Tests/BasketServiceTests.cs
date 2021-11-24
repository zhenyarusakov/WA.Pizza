using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.Services;
using Xunit;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.Tests
{
    public class BasketServiceTests
    {

        [Fact]
        public async Task GetBasket_AllBasketReturned()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WAPizzaContext>()
                .UseInMemoryDatabase(databaseName: "WA.Pizza")
                .Options;

            await using var context = new WAPizzaContext(options);

            context.Baskets.Add(new Basket
            {
                Id = 1,
                Name = "qwe",
                UserId = 1
            });

            context.SaveChanges();

            // Act
            var basketService = new BasketService(context);

            var baskets = await basketService.GetBasketsAsync();

            // Assert
            baskets.Should().NotBeNullOrEmpty();

            var actualBasket = baskets.Single();

            actualBasket.Id.Should().Be(1);
            actualBasket.Name.Should().Be("qwe");
        }

        [Fact]
        public async Task UpdateBasket_Updating_the_bucket_name()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WAPizzaContext>()
                .UseInMemoryDatabase(databaseName: "WA.Pizza")
                .Options;

            await using var context = new WAPizzaContext(options);

            context.Baskets.Add(new Basket
            {
                Id = 1,
                Name = "qwe",
                UserId = 1
            });

            var basket = new UpdateBasketRequest(1, "qwee", 1, new List<BasketItemDto>
            {
                new BasketItemDto()
                {
                    Quantity = 10,
                    Price = 12,
                    Description = "qwe",
                    Name = "qwe"
                }
            });
            
            // Act
            var basketService = new BasketService(context);

            var updateBasket = await basketService.UpdateBasketAsync(basket);

            // Assert
            updateBasket.Should().NotBeNull();
            updateBasket.Id.Should().Be(1);
            updateBasket.Name.Should().Be("qwee");
        }

    }
}
