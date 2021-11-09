using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public ICollection<Merch> Merches { get; set; }
    }
}
