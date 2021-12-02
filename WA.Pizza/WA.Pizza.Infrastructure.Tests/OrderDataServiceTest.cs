using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.MapperConfiguration;
using WA.Pizza.Infrastructure.Data.Services;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;
using WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;
using Xunit;

namespace WA.Pizza.Infrastructure.Tests
{
    public class OrderDataServiceTest
    {
        public OrderDataServiceTest()
        {
            MapperGlobal.Configure();
        }

        [Fact]
        public async Task Succeed_return_all_existed_Orders()
        {
            //Arrange
            ICollection<Order> orders = OrderHelper.CreateListOfFilledOrders();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();

            OrderDataService orderService = new OrderDataService(context, new BasketDataService(context));

            //Act
            OrderDto[] allOrders = await orderService.GetAllOrdersAsync();

            //Assert
            allOrders.Should().HaveCount(orders.Count());
        }

        [Fact]
        public async Task Checking_the_equality_value_Basket_and_Order()
        {
            // Arrange
            ICollection<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.AddRange(baskets);
            await context.SaveChangesAsync();
            Basket basket = baskets.First();
            OrderDataService orderDataService = new OrderDataService(context, new BasketDataService(context));

            Dictionary<int, int> basketItems = basket.BasketItems.ToDictionary(x => x.CatalogItemId, x => x.Quantity);
            await context.SaveChangesAsync();

            // Act
            int orderId = await orderDataService.CreateOrderAsync(basket.Id, basket.UserId.GetValueOrDefault());

            Order order = await context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            
            Dictionary<int, int> orderItems = order!.OrderItems.ToDictionary(x => x.CatalogItemId, x => x.Quantity);

            // Assert
            foreach (KeyValuePair<int, int> basketItem in basketItems)
            {
                bool isOrderItemExists = orderItems.TryGetValue(basketItem.Key, out int orderItemQuantity);

                isOrderItemExists.Should().BeTrue();

                bool isQuantityMatch = basketItem.Value == orderItemQuantity;

                isQuantityMatch.Should().BeTrue();
            }
        }

        [Fact]
        public async Task Success_order_creation()
        {
            // Arrange
            ICollection<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.AddRange(baskets);
            await context.SaveChangesAsync();
            OrderDataService orderDataService = new OrderDataService(context, new BasketDataService(context));
            Basket basket = baskets.First();

            // Act
            int orderId = await orderDataService.CreateOrderAsync(basket.Id, basket.UserId.GetValueOrDefault());

            // Assert
            Order order = await context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            order.Should().NotBeNull();
            order.Should().Be(basket.UserId);
            order!.OrderItems.Should().BeEquivalentTo(
                basket.BasketItems,
                options => options.Excluding(x => x.Id).ExcludingMissingMembers());
        }

        [Fact]
        public async Task Exception_will_be_thrown_when_creating_order_with_nonexistent_Id()
        {
            // Arrange
            ICollection<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.AddRange(baskets);
            await context.SaveChangesAsync();
            OrderDataService orderDataService = new OrderDataService(context, new BasketDataService(context));
            int basketId = baskets.First().Id + 5;
            Basket basket = baskets.First();

            // Act
            Func<Task> func = async () => await orderDataService.CreateOrderAsync(basketId, basket.UserId.GetValueOrDefault());

            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Checking_for_the_presence_of_BasketItems()
        {
            // Arrange
            ICollection<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Baskets.AddRange(baskets);
            await context.SaveChangesAsync();
            OrderDataService orderDataService = new OrderDataService(context, new BasketDataService(context));

            // Act
            IEnumerable<ICollection<BasketItem>> catalogItemIds = baskets.Select(x => x.BasketItems);

            // Assert
            catalogItemIds.Should().HaveCount(baskets.Count());
        }

        [Fact]
        public async Task Exception_will_be_thrown_when_creating_order_with_nonexistent_CatalogItemId()
        {
            // Arrange
            CatalogItem catalogItem = new CatalogItem
            {
                Id = 6,
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
                        CatalogItemId = 1,
                        Quantity = 1
                    }
                }
            };

            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItem);
            context.Baskets.AddRange(basket);
            await context.SaveChangesAsync();
            OrderDataService orderDataService = new OrderDataService(context, new BasketDataService(context));

            // Act
            Func<Task> func = async () => await orderDataService.CreateOrderAsync(basket.Id, basket.UserId.GetValueOrDefault());

            // Assert
            await func.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Exception_will_thrown_if_the_number_items_exceeds_the_allowed_value()
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
                        CatalogItemId = 1,
                        Quantity = 15
                    }
                }
            };

            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.CatalogItems.AddRange(catalogItem);
            context.Baskets.AddRange(basket);
            await context.SaveChangesAsync();
            OrderDataService orderDataService = new OrderDataService(context, new BasketDataService(context));

            // Act
            Func<Task> func = async () => await orderDataService.CreateOrderAsync(basket.Id, basket.UserId.GetValueOrDefault());

            // Assert
            await func.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Successfully_return_updated_order_status()
        {
            // Arrange
            Order filledOrders = OrderHelper.CreateOneFilledOrders();
            await using WAPizzaContext context = await DbContextFactory.CreateContext();
            context.Orders.AddRange(filledOrders);
            await context.SaveChangesAsync();
            OrderDataService orderService = new OrderDataService(context, new BasketDataService(context));
            int orderId = filledOrders.Id;
            OrderStatus expectedStatus = OrderStatus.Dispatch;

            // Act
            int statusId = await orderService.UpdateOrderStatus(orderId, expectedStatus);

            // Assert
            Order order = await context.Orders.FirstOrDefaultAsync(x => x.Id == statusId);
            order!.Status.Should().Be(expectedStatus);
        }
    }
}
