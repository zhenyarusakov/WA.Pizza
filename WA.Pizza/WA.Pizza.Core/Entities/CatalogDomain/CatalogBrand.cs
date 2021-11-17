using System.Collections.Generic;

namespace WA.Pizza.Core.Entities.CatalogDomain
{
    public class CatalogBrand : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Catalog> CatalogItems { get; set; }
    }
}
