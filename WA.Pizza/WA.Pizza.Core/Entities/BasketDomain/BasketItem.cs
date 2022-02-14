using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Core.Entities.BasketDomain
{
    public class BasketItem: BaseEntity
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; init; }
        public CatalogItem? CatalogItem { get; init; }
        public int BasketId { get; set; }
        public Basket? Basket { get; init; }
    }
}
