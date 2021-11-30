using System.Collections.Generic;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers
{
    public static class CatalogHelper
    {
        public static IEnumerable<CatalogItem> CreateListOfFilledCatalogItems()
        {
            List<CatalogItem> catalogs = new List<CatalogItem>();

            catalogs.Add(new CatalogItem
            {
                Name = "Desserts",
                Description = "Desserts",
                Price = 12,
                Quantity = 10,
                CatalogType = CatalogType.Desserts
            });

            catalogs.Add(new CatalogItem
            {
                Name = "Drinks",
                Description = "Drinks",
                Price = 12,
                Quantity = 10,
                CatalogType = CatalogType.Drinks
            });

            catalogs.Add(new CatalogItem
            {
                Name = "Snacks",
                Description = "Snacks",
                Price = 12,
                Quantity = 10,
                CatalogType = CatalogType.Snacks
            });

            return catalogs;
        }
    }
}
