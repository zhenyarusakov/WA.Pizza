using System.Threading.Tasks;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IOrderDataService
    {
        Task<OrderDto[]> GetAllOrdersAsync();
        Task<int> CreateOrderAsync(int basketId, string userId);
        Task<int> UpdateOrderStatus(int id, OrderStatus status);
    }
}
