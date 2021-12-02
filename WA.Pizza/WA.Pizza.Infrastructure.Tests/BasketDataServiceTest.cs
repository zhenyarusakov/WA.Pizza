using Xunit;
using System;
using System.Linq;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.Xunit2;
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
                BasketItems = new List<BasketItemDto>(new []{basketItemDto})
            };

            BasketDataService basketService = new(context);

            // Act
            int basketId = await basketService.CreateBasketAsync(createBasketRequest);

            Basket basket = await context.Baskets.Include(i => i.BasketItems).SingleOrDefaultAsync(i => i.Id == basketId);

            // Assert
            basket.Should().NotBeNull();

            basket!.BasketItems.Should()
                .HaveCount(createBasketRequest.BasketItems.Count)
                .And
                .ContainEquivalentOf(basketItemDto, opt => opt.Excluding(i => i.BasketId).Excluding(x => x.Id));
        }

        [Fact]
        public async Task Success_change_of_item_quantity()
        {
            // Arrange
            ICollection<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.AddRange(baskets);
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
            context.BasketItems.Add(basketItem);
            await context.SaveChangesAsync();
            BasketDataService basketService = new BasketDataService(context);
            UpdateBasketItemRequest basketItemRequest = new UpdateBasketItemRequest
            {
                Id = basketItem.Id + 5,
                Name = basketItem.Name,
                Quantity = basketItem.Quantity
            };

            // Act
            Func<Task> func = async () => await basketService.UpdateBasketItemAsync(basketItemRequest);

            // Assert
            await func.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Not_possible_create_BasketItem_with_quantity_equal_or_less_zero()
        {
            // Arrange
            Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Add(basket);
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
            context.Baskets.Add(basket);
            await context.SaveChangesAsync();
            BasketDataService basketService = new BasketDataService(context);
            int basketItemsId = basket.BasketItems.First().Id;

            // Act
            await basketService.CleanBasketAsync(basketItemsId);

            // Assert
            basket.BasketItems.Should().BeEmpty();

        }

        [Fact]
        public async Task Return_error_with_non_existent_id()
        {
            // Arrange
            Basket basket = BasketHelpers.CreateOneFilledShoppingBasket();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.Add(basket);
            await context.SaveChangesAsync();
            BasketDataService basketService = new BasketDataService(context);

            // Act
            Func<Task> func = async () => await basketService.CleanBasketAsync(basket.Id + 5);

            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

    }
}
