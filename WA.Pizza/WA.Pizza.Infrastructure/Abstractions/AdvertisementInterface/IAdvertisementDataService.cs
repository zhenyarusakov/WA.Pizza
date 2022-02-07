using System;
using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.AdvertisingDTO;

namespace WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;

public interface IAdvertisementDataService
{
    Task<int> CreateAdvertisementAsync(CreateAdvertisingRequest createAdvertisingRequest, Guid apiKey);
    Task<AdvertisingDto[]> GetAllAdvertisementAsync(Guid apiKey);
    Task<AdvertisingDto> GetOneAdvertisementAsync(int id, Guid apiKey);
    Task<int> UpdateAdvertisementAsync(UpdateAdvertisingRequest updateAdvertisingRequest, Guid apiKey);
    Task<int> RemoveAdvertisementAsync(int id, Guid apiKey);
}