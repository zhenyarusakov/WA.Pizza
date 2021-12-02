using AutoFixture;
using AutoFixture.Xunit2;

namespace WA.Pizza.Infrastructure.Tests.Customizations
{
    internal class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute() : base(() => new Fixture().Customize(new CompositeCustomization(new WACustomization())))
        {
        }
    }
}
