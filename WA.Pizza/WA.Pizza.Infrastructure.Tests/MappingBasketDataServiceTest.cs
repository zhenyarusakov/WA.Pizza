using Xunit;
using Mapster;
using FluentAssertions;
using System.Collections.Generic;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.Tests
{
    public class MappingBasketDataServiceTest
    {
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
    }
}
