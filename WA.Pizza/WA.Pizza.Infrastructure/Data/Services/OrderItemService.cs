using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class OrderItemService: IOrderItemService
    {
        private readonly WAPizzaContext _context;
        public OrderItemService(WAPizzaContext context)
        {
            _context = context;
        }

        public Task<OrderItem> GetOrderItemAsync(int id)
        {
            return _context.OrderItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<OrderItem[]> GetOrderItemsAsync()
        {
            return _context.OrderItems
                .Include(x=>x.Order).AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);

            await _context.SaveChangesAsync();

            return orderItem;
        }

        public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem)
        {
            var orderItemUpdate = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == orderItem.Id);

            if (orderItemUpdate == null)
            {
                throw new ArgumentNullException($"There is no OrderItem with this {orderItem.Id}");
            }

            orderItemUpdate.Description = orderItem.Description;
            orderItemUpdate.Name = orderItem.Name;
            orderItemUpdate.Order = orderItem.Order;
            orderItemUpdate.Price = orderItem.Price;

            _context.Update(orderItemUpdate);

            await _context.SaveChangesAsync();

            return orderItemUpdate;
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            _context.OrderItems.Remove(new OrderItem()
            {
                Id = id
            });

            await _context.SaveChangesAsync();
        }
    }
}
