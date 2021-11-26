using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.Basket
{
    public record CreateBasketRequest(ICollection<BasketItemDto> BasketItems)
    {
        public int? UserId { get; set; }
    }
}
