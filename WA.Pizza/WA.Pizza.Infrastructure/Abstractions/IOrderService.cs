using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderAsync(int id);
        Task<OrderDto[]> GetOrdersAsync();
        Task<OrderDto> CreateOrderAsync(CreateOrUpdateOrderRequest orderRequest);
        Task<OrderDto> UpdateOrderAsync(CreateOrUpdateOrderRequest orderRequest);
        Task UpdateOrderStatus(UpdateOrderStatusDto orderRequest);
        Task DeleteOrderAsync(int id);
    }
}
