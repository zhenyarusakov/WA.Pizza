using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities.BasketDomain
{
    public class Basket: BaseEntity
    {
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime? LastModified { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
