namespace WA.Pizza.Core.Entities.CatalogDomain
{
    public class CatalogItem : BaseEntity
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal? Price { get; init; }
        public int Quantity { get; set; }
        public int? CatalogBrandId { get; init; }
        public CatalogBrand? CatalogBrand { get; init; }
        public CatalogType? CatalogType { get; init; }
    }
}
