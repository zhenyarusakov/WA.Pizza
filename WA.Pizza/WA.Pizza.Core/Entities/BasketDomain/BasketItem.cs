using System;
using WA.Pizza.Core.Entities.BasketDomain;

namespace WA.Pizza.Core.Entities
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
