using System;
using System.Collections.Generic;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers
{
    public static class BasketHelpers
    {
        public static IEnumerable<Basket> CreateListOfFilledBaskets()
        {
            List<Basket> baskets = new List<Basket>();

            baskets.Add(new Basket
            {
                LastModified = DateTime.Now,
                BasketItems = new List<BasketItem>
                {
                    new BasketItem
                    {
                        Quantity = 2,
                        Description = "Dessert",
                        Name = "Dessert",
                        Price = 12,
                        CatalogItem = new CatalogItem
                        {
                            Quantity = 10,
                            Description = "Dessert",
                            Name = "Dessert",
                            Price = 12,
                            CatalogType = CatalogType.Desserts,
                            CatalogBrand = new CatalogBrand
                            {
                                Description = "Dessert",
                                Name = "Dessert"
                            }
                        }
                    }
                }
            });

            baskets.Add(new Basket
            {
                LastModified = DateTime.Now,
                BasketItems = new List<BasketItem>
                {
                    new BasketItem
                    {
                        Quantity = 3,
                        Description = "Drink",
                        Name = "Drink",
                        Price = 13,
                        CatalogItem = new CatalogItem
                        {
                            Quantity = 10,
                            Description = "Drink",
                            Name = "Drink",
                            Price = 13,
                            CatalogType = CatalogType.Drinks,
                            CatalogBrand = new CatalogBrand
                            {
                                Description = "Drink",
                                Name = "Drink"
                            }
                        }
                    }
                }
            });

            return baskets;
        }



        public static Basket CreateOneEmptyShoppingCart()
        {
            return new Basket
            {
                BasketItems = new List<BasketItem>
                {
                }
            };

        }

        public static Basket CreateOneFilledShoppingBasket()
        {
            return new Basket
            {
                LastModified = DateTime.Now,
                BasketItems = new List<BasketItem>
                {
                    new BasketItem
                    {
                        Quantity = 2,
                        Description = "Dessert",
                        Name = "Dessert",
                        Price = 12,
                        CatalogItem = new CatalogItem
                        {
                            Quantity = 10,
                            Description = "Dessert",
                            Name = "Dessert",
                            Price = 12,
                            CatalogType = CatalogType.Desserts,
                            CatalogBrand = new CatalogBrand
                            {
                                Description = "Dessert",
                                Name = "Dessert"
                            }
                        }
                    }
                }
            };
        }
    }
}
