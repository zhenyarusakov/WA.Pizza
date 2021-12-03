using Xunit;
using System.Linq;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using WA.Pizza.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.Data.Services;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;
using WA.Pizza.Infrastructure.Data.MapperConfiguration;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;

namespace WA.Pizza.Infrastructure.Tests
{
    public class BasketDataServiceTest
    {
        public BasketDataServiceTest()
        {
            MapperGlobal.Configure();
        }

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
            int basketsCount = context.BasketItems.Count();
            basketsCount!.Should().Be(baskets.Count);
        }

        [Fact]
        public async Task Create_not_empty_basket_success()
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();

            BasketItemDto basketItemDto = new ()
            {
                Name = "Name",
                Price = 12,
                Quantity = 2
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
            Basket item = new ()
            {
                BasketItems = new List<BasketItem>()
                {
                    new ()
                    {
                        Quantity = 2,
                        Description = "Dessert",
                        Name = "Dessert",
                        Price = 12,
                        Basket = new Basket()
                    }
                }
            };
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.AddRange(item);
            await context.SaveChangesAsync();

            int newQuantity = item.BasketItems.First().Quantity + 1;
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
            
            Basket basketItem = new Basket
            {
                BasketItems = new List<BasketItem>
                {
                    new BasketItem()
                    {
                        Quantity = 2,
                        Basket = new Basket(),
                        Description = "qwe",
                        Name = "qwe"
                    }
                }
            };

            context.Add(basketItem);
            await context.SaveChangesAsync();
            BasketDataService basketService = new (context);

            UpdateBasketItemRequest basketRequest = new ()
            {
                Id = basketItem.Id,
                Name = "qwe",
                Quantity = 0,
            };

            // Act
            int basketId =  await basketService.UpdateBasketItemAsync(basketRequest);

            // Assert
            BasketItem item = await context.BasketItems.FirstOrDefaultAsync(x => x.Id == basketId);
            item!.Should().BeNull();
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