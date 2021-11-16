using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderAsync(int id);
        Task<OrderDto[]> GetOrdersAsync();
        Task<OrderDto> CreateOrderAsync(OrderForModifyDto modifyDto);
        Task<OrderDto> UpdateOrderAsync(OrderForModifyDto modifyDto);
        Task DeleteOrderAsync(int id);
    }
}
