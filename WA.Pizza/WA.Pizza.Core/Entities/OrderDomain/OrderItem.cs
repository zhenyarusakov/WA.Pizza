namespace WA.Pizza.Core.Entities.OrderDomain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
