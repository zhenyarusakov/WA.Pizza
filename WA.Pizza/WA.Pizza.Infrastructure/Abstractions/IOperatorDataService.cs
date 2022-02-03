using System;
using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.AdsClientDTO;

namespace WA.Pizza.Infrastructure.Abstractions;

public interface IOperatorDataService
{
    Task<Guid> CreateNewAdsClientAsync(CreateAdsClientRequest adsClientRequest);
    Task RemoveAdsClientAsync(int id);
    // Task<int> BlockClientAsync(int id);
}