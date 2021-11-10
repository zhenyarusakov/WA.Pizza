using System;

namespace WA.Pizza.Core.Entities.OrderDomain
{
    // Заказанный товар
    public class OrderItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public Guid OrderId { get; set; }
        // Заказ, в котором содержится данный заказанный товар
        public Order Order { get; set; }
    }
}
