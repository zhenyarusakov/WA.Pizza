using Xunit;
using System;
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
using WA.Pizza.Infrastructure.Tests.Customizations;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;

namespace WA.Pizza.Infrastructure.Tests
{
    public class BasketDataServiceTest
    {
        [Fact]
        public async Task Return_all_existed_baskets_success()
        {
            // Arrange
            ICollection<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.AddRange(baskets);
            await context.SaveChangesAsync();

            BasketDataService basketService = new (context);

            // Act
            BasketDto[] allBaskets = await basketService.GetAllBasketsAsync();
            
            // Assert
            allBaskets.Should().HaveCount(baskets.Count);
        }

        [Theory, CustomAutoData]
        public async Task Create_not_empty_basket_success(CatalogItem catalogItem)
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();

            context.CatalogItems.Add(catalogItem);
            await context.SaveChangesAsync();

            BasketItemDto basketItemDto = new ()
            {
                CatalogItemId = catalogItem.Id,
                Name = catalogItem.Name,
                Price = catalogItem.Price,
                Quantity = catalogItem.Quantity
            };
            CreateBasketRequest createBasketRequest = new ()
            {
                BasketItems = new List<BasketItemDto>(new []{ basketItemDto })
            };

            BasketDataService basketService = new(context);

            // Act
            int basketId = await basketService.CreateBasketAsync(createBasketRequest);
            
            // Assert
            Basket basket = await context.Baskets.Include(i => i.BasketItems).FirstOrDefaultAsync(i => i.Id == basketId);
            basket.Should().NotBeNull();
            basket!.BasketItems.Should()
                .HaveCount(createBasketRequest.BasketItems.Count)
                .And
                .ContainEquivalentOf(basketItemDto, opt => opt.Excluding(i => i.BasketId).Excluding(x => x.Id));
        }

        [Fact]
        public async Task Change_item_quantity_success()
        {
            // Arrange
            BasketItem item = new()
            {
                Quantity = 2,
                Description = "Dessert",
                Name = "Dessert",
                Price = 12,
                Basket = new Basket()
            };

            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.BasketItems.AddRange(item);
            await context.SaveChangesAsync();

            int newQuantity = item.Quantity + 1;
            UpdateBasketItemRequest updateBasketRequest = new()
            {
                Id = item.Id,
                Quantity = newQuantity
            };

            BasketDataService basketService = new (context);

            // Act
            int basketItemId = await basketService.UpdateBasketItemAsync(updateBasketRequest);

            // Assert
            BasketItem basketItem = await context.BasketItems.FirstOrDefaultAsync(i => i.Id == basketItemId);
            basketItem.Should().NotBeNull();
            basketItem!.Quantity.Should().Be(newQuantity);
        }

        [Fact]
        public async Task Not_possible_create_BasketItem_with_quantity_equal_or_less_zero()
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();

            BasketItem basket = new ()
            {
                Quantity = 2,
                Name = "Dessert",
            };

            context.Add(basket);
            await context.SaveChangesAsync();
            BasketDataService basketService = new (context);

            UpdateBasketItemRequest basketRequest = new ()
            {
                Id = basket.Id,
                Quantity = 0,
            };

            // Act
            Func<Task> act = async () => await basketService.UpdateBasketItemAsync(basketRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Cleaning_basket_items_success()
        {
            // Arrange
            Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.Add(basket);
            await context.SaveChangesAsync();
            BasketDataService basketService = new(context);

            // Act
            await basketService.CleanBasketAsync(basket.Id);

            // Assert
            Basket items = await context.Baskets.Include(i => i.BasketItems)
                .FirstOrDefaultAsync(i => i.Id == basket.Id);
            items!.BasketItems.Should().BeEmpty();
        }
    }
}