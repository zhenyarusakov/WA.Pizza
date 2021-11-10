using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities.OrderDomain
{
    // Заказ
    public class Order
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public OrderStatus Status { get; set; }
        // Заказанные товары (пицца, сок, снэки)
        ICollection<OrderItem> OrderItems { get; set; }
    }
}
