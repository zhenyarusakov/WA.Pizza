using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.AdvertisingDTO;

namespace WA.Pizza.Infrastructure.Abstractions;

public interface IAdvertisingService
{
    Task<int> CreateAdvertisingAsync(CreateAdvertisingRequest createAdvertisingRequest);
    Task<AdvertisingDto[]> GetAllAdvertisingAsync();
    Task<AdvertisingDto> GetOneAdvertisingAsync(int id);
    Task<int> UpdateAdvertisingAsync(UpdateAdvertisingRequest updateAdvertisingRequest);
    Task RemoveAdvertisingAsync(int id);
}