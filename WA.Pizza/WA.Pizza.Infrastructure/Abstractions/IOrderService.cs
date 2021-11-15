using System.Threading.Tasks;
using WA.Pizza.Core.Entities.OrderDomain;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IOrderService
    {
        Task<Order> GetOrderAsync(int id);
        Task<Order[]> GetOrdersAsync();
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(int id, Order order);
        Task DeleteOrderAsync(int id);
    }
}
