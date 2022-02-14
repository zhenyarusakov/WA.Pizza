using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Address;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;

namespace WA.Pizza.Infrastructure.DTO.UserDTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public ICollection<AddressDto> Addresses { get; set; } = new List<AddressDto>();
        public ICollection<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}
