using System;
using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.Basket
{
    public record UpdateBasketRequest(int Id, DateTime? LastModified, int? UserId, ICollection<BasketItemDto> BasketItems);
}
