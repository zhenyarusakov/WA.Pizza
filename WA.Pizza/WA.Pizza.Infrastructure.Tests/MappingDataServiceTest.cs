using Xunit;
using Mapster;
using FluentAssertions;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.Data.MapperConfiguration;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

namespace WA.Pizza.Infrastructure.Tests
{
    public class MappingDataServiceTest
    {
        public MappingDataServiceTest()
        {
            MapperGlobal.Configure();
        }

        [Fact]
        public void Checking_mapping_of_UpdateBasketRequest_to_Basket()
        {
            // Arrange
            UpdateBasketRequest updateBasket = new UpdateBasketRequest();

            // Act
            Basket mapped = updateBasket.Adapt<Basket>();

            // Assert
            mapped.Should().BeEquivalentTo(updateBasket);
        }

        [Fact]
        public void Checking_mapping_of_UpdateBasketRequest_to_BasketItemDto()
        {
            // Arrange
            UpdateBasketRequest updateBasket = new UpdateBasketRequest();

            // Act
            BasketDto mapped = updateBasket.Adapt<BasketDto>();

            // Assert
            mapped.Should().BeEquivalentTo(updateBasket);
        }

        [Fact]
        public void Checking_mapping_of_BasketItem_to_OrderItem()
        {
            // Arrange
            BasketItem basketItem = new BasketItem
            {
                Name = "qwe",
                Description = "qwe",
                Price = 12,
                Quantity = 2,
                CatalogItemId = 1,
                CatalogItem = new CatalogItem
                {
                    Name = "qwe",
                    Description = "qwe",
                    Price = 12,
                    Quantity = 2
                }
            };

            // Act
            OrderItem orderItem = basketItem.Adapt<OrderItem>();

            // Assert
            orderItem.Should().BeEquivalentTo(
                basketItem, 
                options => options
                    .Excluding(x => x.Id)
                    .Excluding(x => x.CatalogItem)
                    .ExcludingMissingMembers());
        }
    }
}
