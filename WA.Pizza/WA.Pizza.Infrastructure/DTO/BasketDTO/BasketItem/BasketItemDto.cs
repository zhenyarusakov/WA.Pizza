using System.Dynamic;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem
{
    public class BasketItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
    }
}
