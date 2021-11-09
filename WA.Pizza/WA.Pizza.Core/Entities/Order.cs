using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        ICollection<Merch> Merches { get; set; }
    }
}
