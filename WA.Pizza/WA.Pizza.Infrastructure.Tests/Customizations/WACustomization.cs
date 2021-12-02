using AutoFixture;
using WA.Pizza.Core.Entities.CatalogDomain;

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
        }
    }
}
