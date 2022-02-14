using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.Basket
{
    public record CreateBasketRequest
    {
        public int UserId { get; set; }
        public ICollection<BasketItemDto> BasketItems { get; init; } = new List<BasketItemDto>();
    }
}
