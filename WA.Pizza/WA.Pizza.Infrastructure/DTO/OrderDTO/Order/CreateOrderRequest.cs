using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem;

namespace WA.Pizza.Infrastructure.DTO.OrderDTO.Order
{
    public record CreateOrderRequest
    {
        public int UserId { get; init; }
        public ICollection<OrderItemDto> OrderItems { get; init; }
    }
}
