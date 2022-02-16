using System.Collections.Generic;

namespace WA.Pizza.Core.Entities.OrderDomain
{
    public class Order : BaseEntity
    {
        public string? UserId { get; init; }
        public ApplicationUser? User { get; set; }
        public OrderStatus? Status { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
