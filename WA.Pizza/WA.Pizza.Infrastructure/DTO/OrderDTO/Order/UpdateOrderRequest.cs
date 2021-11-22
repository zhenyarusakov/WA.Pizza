using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem;

namespace WA.Pizza.Infrastructure.DTO.OrderDTO.Order
{
    public record UpdateOrderRequest(int Id, string Name, int UserId, ICollection<OrderItemDto> OrderItems);
}
