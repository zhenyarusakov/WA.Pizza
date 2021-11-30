using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
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
            // Arrange
            IEnumerable<Order> orders = OrderHelper.CreateListOfFilledOrders();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(orders);
            OrderDataService orderService = new OrderDataService(context, new BasketDataService(context));
            int ordersId = 2;

            // Act
            OrderDto[] orderDtos = await orderService.GetAllOrdersAsync();

            // Assert
            orderDtos.Should().HaveCount(ordersId);
        }

        [Fact]
        public async Task Checking_the_equality_of_the_Value_of_the_Basket_and_the_Order()
        {
            // Arrange
            IEnumerable<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(baskets);
            Basket basket = baskets.First();
            OrderDataService orderDataService = new OrderDataService(context, new BasketDataService(context));

            Dictionary<int, int> basketItems = basket.BasketItems.ToDictionary(x => x.CatalogItemId, x => x.Quantity);
            await context.SaveChangesAsync();
            
            // Act
            OrderDto orderDto = await orderDataService.CreateOrderAsync(basket.Id, basket.UserId.GetValueOrDefault());
            Dictionary<int, int> orderItems = orderDto.OrderItemDtos.ToDictionary(x => x.CatalogItemId, x => x.Quantity);

            // Assert
            orderDto.Should().NotBeNull();

            foreach (KeyValuePair<int, int> basketItem in basketItems)
            {
                bool isOrderItemExists = orderItems.TryGetValue(basketItem.Key, out int orderItemQuantity);

                isOrderItemExists.Should().BeTrue();

                bool isQuantityMatch = basketItem.Value == orderItemQuantity;

                isQuantityMatch.Should().BeTrue();
            }
        }

        [Fact]
        public async Task Successful_order_creation()
        {
            // Arrange
            IEnumerable<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(baskets);
            OrderDataService orderDataService = new OrderDataService(context, new BasketDataService(context));
            Basket basket = baskets.First();
            await context.SaveChangesAsync();

            // Act
            OrderDto createOrder = await orderDataService.CreateOrderAsync(basket.Id, basket.UserId.GetValueOrDefault());

            // Assert
            createOrder.Should().NotBeNull();

            createOrder.UserId.Should().Be(basket.UserId);

            createOrder.OrderItemDtos.Should().BeEquivalentTo(
                basket.BasketItems,
                options => options.Excluding(x => x.Id).ExcludingMissingMembers());
        }

        [Fact]
        public async Task Exception_will_be_thrown_when_creating_order_with_nonexistent_Id()
        {
            // Arrange
            IEnumerable<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(baskets);
            OrderDataService orderDataService = new OrderDataService(context, new BasketDataService(context));
            int basketId = 5;
            int userId = 1;

            // Act
            Func<Task> func = async () => await orderDataService.CreateOrderAsync(basketId, userId);

            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Checking_for_the_presence_of_BasketItems()
        {
            // Arrange
            IEnumerable<Basket> baskets = BasketHelpers.CreateListOfFilledBaskets();
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(baskets);
            OrderDataService orderDataService = new OrderDataService(context, new BasketDataService(context));

            // Act
            IEnumerable<ICollection<BasketItem>> catalogItemIds = baskets.Select(x => x.BasketItems);

            // Assert
            catalogItemIds.Should().HaveCount(2);
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

            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(new BaseEntity[] { catalogItem, basket });
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

            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(new BaseEntity[] {catalogItem, basket});
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
            await using WAPizzaContext context = await DbContextFactory.CreateContextInSeedData(new []{ filledOrders });
            OrderDataService orderService = new OrderDataService(context, new BasketDataService(context));
            int orderId = filledOrders.Id;
            OrderStatus expectedStatus = OrderStatus.Dispatch;

            // Act
            Order order = await orderService.UpdateOrderStatus(orderId, expectedStatus);

            // Assert
            order.Status.Should().Be(expectedStatus);
        }
    }
}
