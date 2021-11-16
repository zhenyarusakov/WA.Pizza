using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderStatus;

namespace WA.Pizza.Infrastructure.DTO.OrderDTO.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public OrderStatusDto StatusDto { get; set; }
        public ICollection<OrderItemDto> OrderItemDtos { get; set; }

    }
}
