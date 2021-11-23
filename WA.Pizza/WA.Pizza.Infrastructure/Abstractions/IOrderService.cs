using System.Threading.Tasks;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IOrderService
    {
        Task<OrderDto[]> GetOrdersAsync();
        Task<OrderDto> CreateOrderAsync(int basketId, int userId);
        Task<Order> UpdateOrderStatus(int id, OrderStatus status);
    }
}
