using System;
using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.AdvertisementDTO;

namespace WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;

public interface IAdvertisementDataService
{
    Task<int> CreateAdvertisementAsync(CreateAdvertisementRequest createAdvertisementRequest);
    Task<AdvertisementDto[]> GetAllAdvertisementAsync(Guid apiKey);
    Task<AdvertisementDto> GetOneAdvertisementAsync(int id);
    Task<int> UpdateAdvertisementAsync(UpdateAdvertisementRequest updateAdvertisementRequest);
    Task<int> RemoveAdvertisementAsync(int id);
    Task<bool> ApiKeyIsValid(Guid apiKey);
}