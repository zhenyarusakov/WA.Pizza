using System.Collections.Generic;

namespace WA.Pizza.Core.Entities.OrderDomain
{
    public class Order : BaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
