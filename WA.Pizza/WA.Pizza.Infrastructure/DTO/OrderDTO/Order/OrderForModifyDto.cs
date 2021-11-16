using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderStatus;

namespace WA.Pizza.Infrastructure.DTO.OrderDTO.Order
{
    public class OrderForModifyDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public OrderStatusDto StatusDto { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
