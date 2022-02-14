using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Address;

namespace WA.Pizza.Infrastructure.DTO.UserDTO
{
    public class UpdateUserRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public ICollection<AddressDto> Addresses { get; set; } = new List<AddressDto>();
    }
}
