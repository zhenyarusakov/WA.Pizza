using System;
using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.AdvertisementDTO;

namespace WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;

public interface IAdvertisementDataService
{
    Task<int> CreateAdvertisementAsync(CreateAdvertisementRequest createAdvertisementRequest, Guid apiKey);
    Task<AdvertisementDto[]> GetAllAdvertisementAsync(Guid apiKey);
    Task<AdvertisementDto> GetOneAdvertisementAsync(int id, Guid apiKey);
    Task<int> UpdateAdvertisementAsync(UpdateAdvertisementRequest updateAdvertisementRequest, Guid apiKey);
    Task<int> RemoveAdvertisementAsync(int id, Guid apiKey);
    Task<bool> ApiKeyIsValid(Guid apiKey);
}