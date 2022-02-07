using System;
using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.AdsClientDTO;

namespace WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;

public interface IAdsClientDataService
{
    Task<Guid> CreateNewAdsClientAsync(CreateAdsClientRequest adsClientRequest);
    Task RemoveAdsClientAsync(int id);
    Task<int> UpdateAdsClientAsync(UpdateAdsClientRequest adsClientRequest);
    Task<int> BlockClientAsync(int id);
    Task<int> UnlockClientAsync(int id);
}