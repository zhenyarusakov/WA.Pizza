﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly WAPizzaContext _context;
        public OrderService(WAPizzaContext context)
        {
            _context = context;
        }
        
        public Task<OrderDto[]> GetOrdersAsync()
        {
            return _context.Orders.ProjectToType<OrderDto>().ToArrayAsync();
        }

        public async Task<OrderDto> CreateOrderAsync(int basketId, int userId)
        {
            var basket = await _context.Baskets
                .Include(x => x.BasketItems)
                .SingleOrDefaultAsync(x => x.Id == basketId);
            
            if (basket == null)
            {
                throw new ArgumentNullException($"There is no Basket with this {basketId}");
            }
            

            IEnumerable<int> catalogItemIds = basket.BasketItems.Select(x => x.CatalogItemId);
            
            Dictionary<int, CatalogItem> catalogItemsCountById = await _context.CatalogItems
                .Where(x => catalogItemIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id);

            foreach (var basketItem in basket.BasketItems)
            {
                var isInStock = catalogItemsCountById.TryGetValue(basketItem.Id, out CatalogItem catalogItem);

                if (!isInStock)
                {
                    throw new InvalidOperationException($"An catalog item with id {basketItem.CatalogItemId} is missing.");
                }

                catalogItem.Quantity -= basketItem.Quantity;

                if (basketItem.Quantity > catalogItem.Quantity)
                {
                    throw new InvalidOperationException("The number of selected items is greater than the allowed value");
                }
            }

            var order = basket.Adapt<Order>();

            _context.Add(order);

            await _context.SaveChangesAsync();

            return order.Adapt<OrderDto>();
        }

        public async Task<Order> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == orderId);

            order.Status = status;

            await _context.SaveChangesAsync();

            return order;
        }
    }
}
