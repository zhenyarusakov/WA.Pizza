﻿namespace WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem
{
    public class UpdateBasketItemRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int BasketId { get; set; }
    }
}
