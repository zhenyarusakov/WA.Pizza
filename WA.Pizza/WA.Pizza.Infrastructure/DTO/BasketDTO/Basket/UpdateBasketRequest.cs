﻿using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.Basket
{
    public record UpdateBasketRequest(int Id, string Name, int UserId, ICollection<BasketItemDto> BasketItems);
}
