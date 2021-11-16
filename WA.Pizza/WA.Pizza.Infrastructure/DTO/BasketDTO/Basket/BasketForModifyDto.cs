﻿using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;
using WA.Pizza.Infrastructure.DTO.UserDTO;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.Basket
{
    public record BasketForModifyDto(int Id, int UserId, UserDto User, ICollection<BasketItemDto> BasketItems);
}
