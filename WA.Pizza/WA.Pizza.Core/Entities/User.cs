using System.Collections.Generic;
using WA.Pizza.Core.Entities.OrderDomain;

namespace WA.Pizza.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string EntranceNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
