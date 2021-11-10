using WA.Pizza.Core.Entities.BasketDomain;

namespace WA.Pizza.Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
