using System.Collections.Generic;

namespace WA.Pizza.Core.Entities.BasketDomain
{
    public class Basket: BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
