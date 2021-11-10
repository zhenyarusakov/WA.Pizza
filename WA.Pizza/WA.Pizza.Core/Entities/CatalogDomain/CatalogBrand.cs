using System.Collections.Generic;

namespace WA.Pizza.Core.Entities.CatalogDomain
{
    public class CatalogBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CatalogItem> CatalogItems { get; set; }
    }
}
