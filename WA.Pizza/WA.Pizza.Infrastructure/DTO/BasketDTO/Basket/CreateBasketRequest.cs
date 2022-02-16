using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.Basket
{
    public record CreateBasketRequest
    {
        public string? UserId { get; set; }
        public ICollection<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
    }
}
