﻿using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem;

namespace WA.Pizza.Infrastructure.DTO.OrderDTO.Order
{
    public record OrderDto(int Id, string Name, int UserId, ICollection<OrderItemDto> OrderItemDtos);
}
