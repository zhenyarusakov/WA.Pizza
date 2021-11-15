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

        public async Task<OrderItem> GetOrderItemAsync(int id)
        {
            return await _context.OrderItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<OrderItem[]> GetOrderItemsAsync()
        {
            return await _context.OrderItems
                .Include(x=>x.Order).AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);

            await _context.SaveChangesAsync();

            return orderItem;
        }

        public async Task<OrderItem> UpdateOrderItemAsync(int id)
        {
            var orderItemUpdate = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);

            if (orderItemUpdate == null)
            {
                throw new ArgumentNullException($"There is no OrderItem with this {id}");
            }

            _context.Update(orderItemUpdate);

            await _context.SaveChangesAsync();

            return orderItemUpdate;
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItemDelete = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);

            if (orderItemDelete == null)
            {
                throw new ArgumentNullException($"There is no OrderItem with this {id}");
            }

            _context.Remove(orderItemDelete);

            await _context.SaveChangesAsync();
        }
    }
}
