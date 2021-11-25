using System;
using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.Basket
{
    public record CreateBasketRequest(int? UserId, DateTime? LastModified, ICollection<BasketItemDto> BasketItems);
}
