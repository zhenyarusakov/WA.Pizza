using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem
{
    public class BasketItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int BasketId { get; set; }
        public BasketDto Basket { get; set; }
    }
}
