using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderStatus;

namespace WA.Pizza.Infrastructure.DTO.OrderDTO.Order
{
    public record OrderForModifyDto(int Id, string Name, int UserId, OrderStatusDto StatusDto,
        ICollection<OrderItemDto> OrderItems);
}
