using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using WA.Pizza.Core.Entities;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Core.Entities.OrderDomain;

namespace WA.Pizza.Infrastructure.Tests.Customizations
{
    internal class WACustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<CatalogItem>(x => x
                .Without(item => item.Id)
                .Without(item => item.CatalogBrand)
                .Without(item => item.CatalogBrandId));

            fixture.Customize<Basket>(x => x
                .With(item => item.BasketItems, new List<BasketItem>
                {
                    new ()
                    {
                        Quantity = 2,
                        Name = "qwe",
                        Description = "qwe",
                        Price = 12,
                        Basket = new Basket(),
                        CatalogItem = new CatalogItem
                        {
                            Name = "qwe",
                            Quantity = 2
                        }
                    }
                })
                .Without(item => item.LastModified)
                .Without(item => item.UserId));

            fixture.Customize<BasketItem>(x => x
                .Without(item => item.Description)
                .With(item => item.Name, "qwe")
                .Without(item => item.Price)
                .With(item => item.Basket, new Basket()));

            
        }
    }
}
