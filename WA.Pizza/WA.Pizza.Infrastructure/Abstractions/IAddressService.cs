using System.Threading.Tasks;
using WA.Pizza.Core.Entities.OrderDomain;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IAddressService
    {
        Task<Address> GetAddressAsync(int id);
        Task<Address[]> GetAddressesAsync();
        Task<Address> CreateAddressAsync(Address address);
        Task<Address> UpdateAddressAsync(int id);
        Task DeleteAddressAsync(int id);
    }
}
