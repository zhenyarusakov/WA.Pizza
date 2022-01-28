using System;
using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.Basket
{
    public record UpdateBasketRequest
    {
        public int Id { get; init; }
        public DateTime? LastModified { get; init; }
        public ICollection<BasketItemDto> BasketItems { get; init; }
    }
}
