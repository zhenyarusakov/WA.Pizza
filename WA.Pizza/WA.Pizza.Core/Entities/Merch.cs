using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities
{
    public class Merch
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        ICollection<Order> Orders { get; set; }
        ICollection<Catalog> Catalogs { get; set; }
    }
}
