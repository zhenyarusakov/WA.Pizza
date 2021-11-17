﻿using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;
using WA.Pizza.Infrastructure.DTO.UserDTO;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.Basket
{
    public record BasketDto(int Id, int Count, int UserId, UserDto User, ICollection<BasketItemDto> BasketItems);
}
