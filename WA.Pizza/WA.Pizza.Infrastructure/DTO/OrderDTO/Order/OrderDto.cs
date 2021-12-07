using System.Collections.Generic;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem;

namespace WA.Pizza.Infrastructure.DTO.OrderDTO.Order
{
    public record OrderDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public ICollection<OrderItemDto> OrderItemDtos { get; init; }
    }
}
