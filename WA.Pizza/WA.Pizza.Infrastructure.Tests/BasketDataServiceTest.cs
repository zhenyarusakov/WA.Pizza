using Moq;
using Xunit;
using System.Linq;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using WA.Pizza.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.Abstractions;
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
        
        private UserManager<ApplicationUser> UserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            store.Setup(x => x.FindByIdAsync("123", CancellationToken.None))
                .ReturnsAsync(new ApplicationUser()
                {
                    UserName = "test@gmail.com",
                    Id = "123"
                });

            return new UserManager<ApplicationUser>(store.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async Task Return_all_existed_baskets_success()
        {
            // Arrange
            ICollection<Basket> expectedBaskets = BasketHelpers.CreateListOfFilledBaskets();

            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.AddRange(expectedBaskets);
            await context.SaveChangesAsync();
            var basketDataService = new Mock<ILogger<BasketDataService>>().Object;
            var userInfoProvider = new Mock<IUserInfoProvider>().Object;

            BasketDataService basketService = new (context, basketDataService, UserManager(), userInfoProvider);

            // Act
            BasketDto[] actualBaskets = await basketService.GetAllBasketsAsync();

            // Assert
            Basket[] baskets = await context.Baskets.ToArrayAsync();
            actualBaskets.Should().HaveCount(baskets.Count());
            actualBaskets.Should().Equal(baskets, (actual, expected) => 
                actual.Id == expected.Id);
        }

        [Fact]
        public async Task Create_not_empty_basket_success()
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            ICollection<Basket> basket = BasketHelpers.CreateListOfFilledBaskets();
            context.Baskets.AddRange(basket);
            await context.SaveChangesAsync();
            
            CreateBasketItemRequest basketItemRequest = new()
            {
                Name = "admin",
                Description = "admin",
                Price = 11,
                Quantity = 1,
                BasketId = 1,
                CatalogItemId = 1
            };

            var basketDataService = new Mock<ILogger<BasketDataService>>().Object;
            var userInfoProvider = new Mock<IUserInfoProvider>().Object;
            BasketDataService basketService = new (context, basketDataService, UserManager(), userInfoProvider);

            // Act
            int basketId = await basketService.CreateBasketItemAsync(basketItemRequest);
            
            // Assert
            BasketItem? basketItem = await context.BasketItems.FirstOrDefaultAsync(x => x.Id == basketId);
            basketItem.Should().NotBeNull();
            basketItem!.Name.Should().Be(basketItem.Name);
            basketItem.Description.Should().Be(basketItem.Description);
            basketItem.Price.Should().Be(basketItem.Price);
            basketItem.Quantity.Should().Be(basketItem.Quantity);
            basketItem.BasketId.Should().Be(basketItem.BasketId);
            basketItem.CatalogItemId.Should().Be(basketItem.CatalogItemId);
        }

        [Fact]
        public async Task Change_BasketItems_quantity_success()
        {
            // Arrange
            Basket basket = new ()
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
            context.Baskets.Add(basket);
            await context.SaveChangesAsync();

            int newQuantity = basket.BasketItems.First().Quantity + 1;
            UpdateBasketItemRequest updateBasketRequest = new()
            {
                Id = basket.Id,
                Quantity = newQuantity
            };

            var basketDataService = new Mock<ILogger<BasketDataService>>().Object;
            var userInfoProvider = new Mock<IUserInfoProvider>().Object;

            BasketDataService basketService = new (context, basketDataService, UserManager(), userInfoProvider);

            // Act
            int basketItemId = await basketService.UpdateBasketItemAsync(updateBasketRequest);

            // Assert
            BasketItem? basketItem = await context.BasketItems.FirstOrDefaultAsync(i => i.Id == basketItemId);
            basketItem.Should().NotBeNull();
            basketItem!.Quantity.Should().Be(newQuantity);
        }

        [Fact]
        public async Task Not_possible_create_BasketItem_with_quantity_equal_or_less_zero()
        {
            // Arrange
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            
            Basket basket = new Basket
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

            context.Add(basket);
            await context.SaveChangesAsync();
            var basketDataService = new Mock<ILogger<BasketDataService>>().Object;
            var userInfoProvider = new Mock<IUserInfoProvider>().Object;
            BasketDataService basketService = new (context, basketDataService, UserManager(), userInfoProvider);

            UpdateBasketItemRequest basketRequest = new ()
            {
                Id = basket.Id,
                Name = "qwe",
                Quantity = 0,
            };

            // Act
            int basketId =  await basketService.UpdateBasketItemAsync(basketRequest);

            // Assert
            BasketItem? item = await context.BasketItems.FirstOrDefaultAsync(x => x.Id == basketId);
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
            var basketDataService = new Mock<ILogger<BasketDataService>>().Object;
            var userInfoProvider = new Mock<IUserInfoProvider>().Object;

            BasketDataService basketService = new (context, basketDataService, UserManager(), userInfoProvider);

            // Act
            await basketService.CleanBasketAsync(basket.Id);

            // Assert
            Basket? items = await context.Baskets.Include(i => i.BasketItems)
                .FirstOrDefaultAsync(i => i.Id == basket.Id);
            items!.BasketItems.Should().BeEmpty();
        }
    }
}