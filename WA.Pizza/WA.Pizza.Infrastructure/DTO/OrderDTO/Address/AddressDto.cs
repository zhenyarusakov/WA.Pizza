namespace WA.Pizza.Infrastructure.DTO.OrderDTO.Address
{
    public class AddressDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public int Entrance { get; set; }
        public int ApartmentNumber { get; set; }
        public bool isPrimary { get; set; }
    }
}
