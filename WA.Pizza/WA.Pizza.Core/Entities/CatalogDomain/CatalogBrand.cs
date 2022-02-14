using System.Collections.Generic;

namespace WA.Pizza.Core.Entities.CatalogDomain
{
    public class CatalogBrand : BaseEntity
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public ICollection<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
    }
}
