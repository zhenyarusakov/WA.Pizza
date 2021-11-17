﻿using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;

namespace WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem
{
    public class CreateOrUpdateOrderItemRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public OrderDto Order { get; set; }
    }
}
