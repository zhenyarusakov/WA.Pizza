namespace WA.Pizza.Core.Entities.BasketDomain
{
    public class BasketItem: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
