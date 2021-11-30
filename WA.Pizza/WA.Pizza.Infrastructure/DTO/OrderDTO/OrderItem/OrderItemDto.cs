namespace WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CatalogItemId { get; set; }
    }
}
