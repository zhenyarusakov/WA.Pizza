using Moq;
using Xunit;
using System;
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
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.Data.Services;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;
using WA.Pizza.Infrastructure.Data.MapperConfiguration;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;

namespace WA.Pizza.Infrastructure.Tests
{
    public class OrderDataServiceTest
    {
        public OrderDataServiceTest()
        {
            MapperGlobal.Configure();
        }

        private UserManager<ApplicationUser> UserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            store.Setup(x => x.FindByIdAsync("123", CancellationToken.None))
                .ReturnsAsync(new ApplicationUser()
                {
                    UserName = "test@email.com",
                    Id = "123"
                });

            return new UserManager<ApplicationUser>(store.Object, null, null, null, null, null, null, null, null);
        }
        
        [Fact]
        public async Task Return_all_Orders()
        {
            //Arrange
            ICollection<Order> orders = OrderHelper.CreateListOfFilledOrders();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();
            var mockBasketDataService = new Mock<ILogger<BasketDataService>>().Object;
            var mockOrderDataService = new Mock<ILogger<OrderDataService>>().Object;
            var userInfoProvider = new Mock<IUserInfoProvider>().Object;
            OrderDataService orderDataService = new (context, 
                new BasketDataService(context, mockBasketDataService, UserManager(), userInfoProvider), mockOrderDataService);

            //Act
            OrderDto[] allOrders = await orderDataService.GetAllOrdersAsync();

            //Assert
            Order[] contextOrders = await context.Orders.ToArrayAsync();
            allOrders.Should().HaveCount(contextOrders.Length);
            allOrders.Should().Equal(contextOrders, (actual, expected) =>
                actual.Id == expected.Id);
        }

        [Fact]
        public async Task Creation_order_success()
        {
            // Arrange
            ICollection<CatalogItem> catalogItem = CatalogHelper.CreateListOfFilledCatalogItems();
            Basket basket = BasketHelpers.CreateBasket();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItem);
            context.Baskets.AddRange(basket);
            await context.SaveChangesAsync();
            
            BasketItem[] newBasketItem = new BasketItem [basket.BasketItems.Count];
            basket.BasketItems.CopyTo(newBasketItem, 0);
            var mockBasketDataService = new Mock<ILogger<BasketDataService>>().Object;
            var mockOrderDataService = new Mock<ILogger<OrderDataService>>().Object;
            var userInfoProvider = new Mock<IUserInfoProvider>().Object;
            OrderDataService orderDataService = new (context, 
                new BasketDataService(context, mockBasketDataService, UserManager(), userInfoProvider), mockOrderDataService);

            // Act
            int orderId = await orderDataService.CreateOrderAsync(basket.Id, basket.UserId);

            // Assert
            Order? order = await context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            order.Should().NotBeNull();
            order!.OrderItems.Select(x => x.Quantity).Should()
                .BeEquivalentTo(newBasketItem.Select(x=>x.Quantity));
            order.Status.Should().Be(OrderStatus.New);
        }

        [Fact]
        public async Task Not_possible_create_order_with_nonexistent_CatalogItem()
        {
            // Arrange
            CatalogItem catalogItem = new CatalogItem
            {
                Name = "name",
                Description = "description",
                Quantity = 10
            };

            Basket basket = new Basket
            {
                BasketItems = new List<BasketItem>
                {
                    new()
                    {
                        Name = "name",
                        Description = "description",
                        CatalogItemId = -1,
                        Quantity = 1
                    }
                }
            };

            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.Add(catalogItem);
            context.Baskets.Add(basket);
            await context.SaveChangesAsync();
            var mockBasketDataService = new Mock<ILogger<BasketDataService>>().Object;
            var mockOrderDataService = new Mock<ILogger<OrderDataService>>().Object;
            var userInfoProvider = new Mock<IUserInfoProvider>().Object;
            OrderDataService orderDataService = new (context, 
                new BasketDataService(context, mockBasketDataService, UserManager(), userInfoProvider), mockOrderDataService);

            // Act
            Func<Task> func = async () => await orderDataService.CreateOrderAsync(basket.Id, basket.UserId);

            // Assert
            await func.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Return_updated_order_status_success()
        {
            // Arrange
            Order filledOrders = OrderHelper.CreateOneFilledOrders();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Orders.AddRange(filledOrders);
            await context.SaveChangesAsync();
            var mockBasketDataService = new Mock<ILogger<BasketDataService>>().Object;
            var mockOrderDataService = new Mock<ILogger<OrderDataService>>().Object;
            var userInfoProvider = new Mock<IUserInfoProvider>().Object;
            OrderDataService orderDataService = new (context, 
                new BasketDataService(context, mockBasketDataService, UserManager(), userInfoProvider), mockOrderDataService);
            int orderId = filledOrders.Id;
            OrderStatus expectedStatus = OrderStatus.Dispatch;

            // Act
            int statusId = await orderDataService.UpdateOrderStatus(orderId, expectedStatus);

            // Assert
            Order? order = await context.Orders.FirstOrDefaultAsync(x => x.Id == statusId);
            order!.Status.Should().Be(expectedStatus);
        }
    }
}
