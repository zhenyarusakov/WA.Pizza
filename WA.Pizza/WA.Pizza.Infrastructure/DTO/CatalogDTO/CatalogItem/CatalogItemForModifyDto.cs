using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogBrand;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogType;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem
{
    public class CatalogItemForModifyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CatalogBrandId { get; set; }
        public CatalogBrandDto CatalogBrand { get; set; }
        public CatalogTypeDto CatalogType { get; set; }
    }
}
