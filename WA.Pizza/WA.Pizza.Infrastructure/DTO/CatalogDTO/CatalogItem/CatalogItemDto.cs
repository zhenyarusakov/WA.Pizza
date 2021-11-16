using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogBrand;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogType;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem
{
    public record CatalogItemDto(int Id,string Name, string Description, decimal Price, int CatalogBrandId,
        CatalogBrandDto CatalogBrand, CatalogTypeDto CatalogType);
}
