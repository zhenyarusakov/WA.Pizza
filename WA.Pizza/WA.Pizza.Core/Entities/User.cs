using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string EntranceNumber { get; set; }
        public string ApartmentNumber { get; set; }
        ICollection<Order> Orders { get; set; }
    }
}
