using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities
{
    public class Catalog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Merch> Merches { get; set; }

    }
}
