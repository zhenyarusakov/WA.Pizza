using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities.CatalogDomain
{
    public class CatalogBrand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Может быть несколько товаров одного производителя
        public ICollection<CatalogItem> CatalogItems { get; set; }
    }
}
