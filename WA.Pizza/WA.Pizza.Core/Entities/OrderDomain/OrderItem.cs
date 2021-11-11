﻿namespace WA.Pizza.Core.Entities.OrderDomain
{
    public class OrderItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
