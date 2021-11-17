namespace WA.Pizza.Core.Entities.CatalogDomain
{
    public class Catalog : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CatalogBrandId { get; set; }
        public CatalogBrand CatalogBrand { get; set; }
        public CatalogType CatalogType { get; set; }
    }
}
