using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogBrand
{
    public class UpdateCatalogBrandRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<CatalogItemsListItem>? Catalogs { get; set; }
    }
}
