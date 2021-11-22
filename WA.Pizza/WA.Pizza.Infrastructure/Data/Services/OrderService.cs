using System;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
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
