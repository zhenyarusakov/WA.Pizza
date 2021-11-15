using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;
using WA.Pizza.Infrastructure.DTO.UserDTO;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.Basket
{
    public class BasketDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public ICollection<BasketItemDto> BasketItems { get; set; }
    }
}
