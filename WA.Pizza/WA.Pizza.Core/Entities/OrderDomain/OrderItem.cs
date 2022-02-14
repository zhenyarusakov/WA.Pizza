using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Core.Entities.OrderDomain
{
    public class OrderItem : BaseEntity
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
        public int CatalogItemId { get; set; }
        public CatalogItem CatalogItem { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
