using System.Threading.Tasks;
using WA.Pizza.Core.Entities.OrderDomain;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IOrderItemService
    {
        Task<OrderItem> GetOrderItemAsync(int id);
        Task<OrderItem[]> GetOrderItemsAsync();
        Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem);
        Task<OrderItem> UpdateOrderItemAsync(int id, OrderItem orderItem);
        Task DeleteOrderItemAsync(int id);
    }
}
