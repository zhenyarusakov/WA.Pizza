using Xunit;
using System;
using System.Linq;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using WA.Pizza.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;
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
            await context.Baskets.AddRangeAsync(baskets);
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
            CatalogItem catalogItem = new CatalogItem
            {
                CatalogType = CatalogType.Pizza, 
                Name = "Peperoni", 
                Price = 25.1M, 
                Quantity = 50, 
                Description = "123"
            };
            await context.CatalogItems.AddRangeAsync(catalogItem);
            await context.SaveChangesAsync();

            BasketDataService basketService = new BasketDataService(context);
            
            CreateBasketRequest createBasketRequest = new CreateBasketRequest
            {
                BasketItems = new List<BasketItemDto>
                {
                    new()
                    {
                        CatalogItemId = catalogItem.Id, 
                        Name = catalogItem.Name, 
                        Price = catalogItem.Price, 
                        Quantity = catalogItem.Quantity - 48, 
                        Description = catalogItem.Description
                    }
                }
            };

            // Act
            int basketId = await basketService.CreateBasketAsync(createBasketRequest);
            
            // Assert
            Basket basket = await context.Baskets.Include(i => i.BasketItems).FirstOrDefaultAsync(i => i.Id == basketId);
            basket.Should().NotBeNull();
            basket!.BasketItems.Should().NotBeEmpty();
            
            basket!.BasketItems.First().Should().BeEquivalentTo(createBasketRequest.BasketItems.First(),
                opt => opt.Excluding(i => i.BasketId));
        }

        [Fact]
        public async Task Success_change_of_item_quantity()
        {
            // Arrange
            ICollection<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            await context.Baskets.AddRangeAsync(baskets);
            await context.SaveChangesAsync();

            int newQuantity = baskets.First().BasketItems.First().Quantity + 5;
            UpdateBasketItemRequest updateBasketRequest = new UpdateBasketItemRequest
            {
                Id = baskets.First().Id,
                Quantity = newQuantity
            };

            BasketDataService basketService = new BasketDataService(context);
            
            // Act
            int basketId = await basketService.UpdateBasketItemAsync(updateBasketRequest);

            // Assert
            Basket basket = await context.Baskets.Include(i => i.BasketItems).FirstOrDefaultAsync(i => i.Id == basketId);
            basket.Should().NotBeNull();
            basket!.BasketItems.Should().Contain(x => x.Quantity == newQuantity);
        }

        [Fact]
        public async Task Exception_will_returned_for_nonexistent_Id()
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            BasketItem basketItem = new BasketItem
            {
                Name = "Name",
                Price = 12,
                Quantity = 1,
                Description = "Description"
            };
            await context.BasketItems.AddRangeAsync(basketItem);
            await context.SaveChangesAsync();
            BasketDataService basketService = new BasketDataService(context);
            UpdateBasketItemRequest basketItemRequest = new UpdateBasketItemRequest
            {
                Id = basketItem.Id + 5,
                Name = basketItem.Name,
                Quantity = basketItem.Quantity,
                Description = basketItem.Description
            };

            // Act
            Func<Task> func = async () => await basketService.UpdateBasketItemAsync(basketItemRequest);

            // Assert
            await func.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Exception_will_thrown_when_quantity_items_less_than_one()
        {
            // Arrange
            Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            await context.AddRangeAsync(basket);
            await context.SaveChangesAsync();
            BasketDataService basketService = new BasketDataService(context);

            UpdateBasketItemRequest basketRequest = new UpdateBasketItemRequest
            {
                Id = basket.BasketItems.First().Id + 5,
                Quantity = basket.BasketItems.First().Quantity,
                Description = basket.BasketItems.First().Description,
                Name = basket.BasketItems.First().Name,
                Price = basket.BasketItems.First().Price
            };

            // Act
            Func<Task> act = async () => await basketService.UpdateBasketItemAsync(basketRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Success_cleaning_of_basket_items()
        {
            // Arrange
            Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            await context.Baskets.AddRangeAsync(basket);
            await context.SaveChangesAsync();
            BasketDataService basketService = new BasketDataService(context);
            int basketItemsId = basket.BasketItems.First().Id;

            // Act
            await basketService.CleanBasketItemsAsync(1);

            // Assert
            basket.BasketItems.Should().BeEmpty();

        }

        [Fact]
        public async Task Return_error_with_non_existent_id()
        {
            // Arrange
            Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            await context.Baskets.AddRangeAsync(basket);
            await context.SaveChangesAsync();
            BasketDataService basketService = new BasketDataService(context);

            // Act
            Func<Task> func = async () => await basketService.CleanBasketItemsAsync(basket.Id + 5);

            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

    }
}
