namespace WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem
{
    public class UpdateBasketItemRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; init; }
        public int BasketId { get; set; }
        public int CatalogItemId { get; set; }
    }
}
