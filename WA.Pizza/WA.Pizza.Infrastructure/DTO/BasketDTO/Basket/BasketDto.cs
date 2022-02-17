using System;
using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.Basket
{
    public record BasketDto()
    {
        public int Id { get; init; }
        public ICollection<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
        public string? UserId { get; init; }
        public DateTime? LastModified { get; init; }
    }
}
