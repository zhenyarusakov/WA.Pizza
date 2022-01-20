using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class OrderDataService : IOrderDataService
    {
        private readonly WAPizzaContext _context;
        private readonly IBasketDataService _basketDataService;
        private readonly ILogger _logger;
        public OrderDataService(WAPizzaContext context, IBasketDataService basketDataService, ILogger logger)
        {
            _context = context;
            _basketDataService = basketDataService;
            _logger = logger;
        }
        
        public Task<OrderDto[]> GetAllOrdersAsync()
        {
            return _context.Orders.ProjectToType<OrderDto>().ToArrayAsync();
        }

        public async Task<int> CreateOrderAsync(int basketId, int userId)
        {
            Basket basket = await _context.Baskets
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.CatalogItem)
                .FirstOrDefaultAsync(x => x.Id == basketId);

            if (basket == null)
            {
                _logger.Error($"There is no Basket with this {basketId}");
                throw new ArgumentNullException($"There is no Basket with this {basketId}");
            }

            IEnumerable<int> catalogItemIds = basket.BasketItems.Select(x => x.CatalogItemId);

            Dictionary<int, CatalogItem> catalogItemsCountById = await _context.CatalogItems
                .Where(x => catalogItemIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id);

            foreach (BasketItem basketItem in basket.BasketItems)
            {
                bool isInStock = catalogItemsCountById.TryGetValue(basketItem.Id, out CatalogItem catalogItem);

                if (!isInStock)
                {
                    _logger.Error($"An catalog item with id {basketItem.CatalogItemId} is missing.");
                    throw new InvalidOperationException($"An catalog item with id {basketItem.CatalogItemId} is missing.");
                }

                if (basketItem.Quantity > catalogItem.Quantity)
                {
                    _logger.Error($"The number of selected items is greater than the allowed value");
                    throw new InvalidOperationException("The number of selected items is greater than the allowed value");
                }

                catalogItem.Quantity -= basketItem.Quantity;
            }

            Order order = basket.Adapt<Order>();

            order.Status = OrderStatus.New;

            _context.Add(order);

            await _context.SaveChangesAsync();

            await _basketDataService.CleanBasketAsync(basketId);

            return order.Id;
        }

        public async Task<int> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                _logger.Error($"Order not found");
                throw new ArgumentException("Order not found");
            }

            order.Status = status;

            await _context.SaveChangesAsync();

            return order.Id;
        }
    }
}
