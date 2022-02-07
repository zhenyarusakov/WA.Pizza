using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;
using WA.Pizza.Infrastructure.DTO.AdvertisingDTO;
using WA.Pizza.Infrastructure.ErrorHandling;

namespace WA.Pizza.Infrastructure.Data.Services.AdvertisementServices;

public class AdvertisementDataService: IAdvertisementDataService
{
    private readonly WAPizzaContext _context;

    public AdvertisementDataService(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAdvertisementAsync(CreateAdvertisingRequest createAdvertisingRequest, Guid apiKey)
    {
        Advertisement newAdvertisement = createAdvertisingRequest.Adapt<Advertisement>();

        var client = await _context.AdsClients.Where(x=>x.IsBlocked)
            .FirstOrDefaultAsync(x => x.Id == createAdvertisingRequest.AdsClientId);

        if (client == null)
        {
            throw new InvalidException($"This client does not exist.");
        }
        
        if (client.ApiKey != apiKey)
        {
            throw new InvalidException($"invalid ApiKey - {apiKey}");
        }
        
        _context.Advertisements.Add(newAdvertisement);

        await _context.SaveChangesAsync();

        return newAdvertisement.Id;
    }

    public async Task<AdvertisingDto[]> GetAllAdvertisementAsync(Guid apiKey)
    {
        AdvertisingDto[] advertising = await _context.Advertisements
            .Include(x=>x.AdsClient)
            .Where(x => x.AdsClient.ApiKey == apiKey && x.AdsClient.IsBlocked)
            .ProjectToType<AdvertisingDto>()
            .ToArrayAsync();

        if (advertising == null)
        {
            throw new InvalidException($"invalid ApiKey - {apiKey}");
        }

        return advertising;
    }

    public async Task<AdvertisingDto> GetOneAdvertisementAsync(int id, Guid apiKey)
    {
        Advertisement advertisement = await _context.Advertisements
            .AsNoTracking()
            .Where(x=>x.AdsClient.IsBlocked)
            .Include(x => x.AdsClient)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (advertisement == null)
        {
            throw new InvalidException($"This ad does not exist {id}");
        }
        
        if (advertisement.AdsClient.ApiKey != apiKey)
        {
            throw new InvalidException($"invalid ApiKey - {apiKey}");
        }

        return advertisement.Adapt<AdvertisingDto>();
    }

    public async Task<int> UpdateAdvertisementAsync(UpdateAdvertisingRequest updateAdvertisingRequest, Guid apiKey)
    {
        Advertisement advertisement =
            await _context.Advertisements
                .Where(x=>x.AdsClient.IsBlocked)
                .Include(x=>x.AdsClient)
                .FirstOrDefaultAsync(x => x.Id == updateAdvertisingRequest.Id);

        if (advertisement == null)
        {
            throw new InvalidException($"There is no Advertising with this {updateAdvertisingRequest.Id}");
        }
        
        if (advertisement.AdsClient.ApiKey != apiKey)
        {
            throw new InvalidException($"invalid ApiKey - {apiKey}");
        }
        
        updateAdvertisingRequest.Adapt(advertisement);

        _context.Update(advertisement);

        await _context.SaveChangesAsync();

        return advertisement.Id;
    }

    public async Task<int> RemoveAdvertisementAsync(int id, Guid apiKey)
    {
        Advertisement advertisement =
            await _context.Advertisements
                .Where(x=>x.AdsClient.IsBlocked)
                .Include(x=>x.AdsClient)
                .FirstOrDefaultAsync(x => x.Id == id);
        
        if (advertisement == null)
        {
            throw new InvalidException($"There is no Advertising with this {id}");
        }
        
        if (advertisement.AdsClient.ApiKey != apiKey)
        {
            throw new InvalidException($"invalid ApiKey - {apiKey}");
        }

        _context.Advertisements.Remove(advertisement);

        await _context.SaveChangesAsync();

        return advertisement.Id;
    }
}