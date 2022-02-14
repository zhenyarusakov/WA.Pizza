namespace WA.Pizza.Core.Entities.OrderDomain
{
    #nullable disable
    public class Address : BaseEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public int Entrance { get; set; }
        public int ApartmentNumber { get; set; }
        public bool isPrimary { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
