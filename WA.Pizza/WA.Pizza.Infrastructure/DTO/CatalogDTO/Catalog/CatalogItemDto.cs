using System.Collections.Generic;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogBrand;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem
{
    public record CatalogItemDto(int Id, string Name, int Quantity, string Description, decimal Price, 
        int CatalogBrandId, CatalogBrandDto CatalogBrand, ICollection<BasketItem> BasketItems);
}
