using System;
using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.AdvertisingDTO;

namespace WA.Pizza.Infrastructure.Abstractions;

public interface IAdvertisingDataService
{
    Task<int> CreateAdvertisingAsync(CreateAdvertisingRequest createAdvertisingRequest, Guid apiKey);
    Task<AdvertisingDto[]> GetAllAdvertisingAsync(Guid apiKey);
    Task<AdvertisingDto> GetOneAdvertisingAsync(int id, Guid apiKey);
    Task<int> UpdateAdvertisingAsync(UpdateAdvertisingRequest updateAdvertisingRequest, Guid apiKey);
    Task<int> RemoveAdvertisingAsync(int id, Guid apiKey);
}