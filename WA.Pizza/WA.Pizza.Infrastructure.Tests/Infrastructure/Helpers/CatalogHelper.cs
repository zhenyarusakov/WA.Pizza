using System.Collections.Generic;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers
{
    public static class CatalogHelper
    {
        public static ICollection<CatalogItem> CreateListOfFilledCatalogItems()
        {
            List<CatalogItem> catalogs = new List<CatalogItem>();

            catalogs.Add(new CatalogItem
            {
                Id = 1,
                Name = "Desserts",
                Description = "Desserts",
                Price = 12,
                Quantity = 10,
                CatalogType = CatalogType.Desserts
            });

            catalogs.Add(new CatalogItem
            {
                Id = 2,
                Name = "Drinks",
                Description = "Drinks",
                Price = 12,
                Quantity = 15,
                CatalogType = CatalogType.Drinks
            });

            return catalogs;
        }
    }
}
