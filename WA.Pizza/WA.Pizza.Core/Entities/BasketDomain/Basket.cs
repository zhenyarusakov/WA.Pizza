using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities.BasketDomain
{
    public class Basket
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
