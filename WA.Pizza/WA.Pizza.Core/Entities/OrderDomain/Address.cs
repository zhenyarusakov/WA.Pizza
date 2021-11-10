using System;

namespace WA.Pizza.Core.Entities.OrderDomain
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Сiti { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public int Entrance { get; set; }
        public int ApartmentNumber { get; set; }
    }
}
