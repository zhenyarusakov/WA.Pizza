using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;
using WA.Pizza.Infrastructure.DTO.AdvertisementDTO;
using WA.Pizza.Infrastructure.ErrorHandling;

namespace WA.Pizza.Infrastructure.Data.Services.AdvertisementServices;

public class AdvertisementDataService: IAdvertisementDataService
{
    private readonly WAPizzaContext _context;

    public AdvertisementDataService(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAdvertisementAsync(CreateAdvertisementRequest createAdvertisementRequest, Guid apiKey)
    {
        Advertisement newAdvertisement = createAdvertisementRequest.Adapt<Advertisement>();

        var client = await _context.AdsClients.Where(x=>x.IsBlocked)
            .FirstOrDefaultAsync(x => x.Id == createAdvertisementRequest.AdsClientId);

        if (client == null)
        {
            throw new InvalidException($"This client does not exist.");
        }
        
        _context.Advertisements.Add(newAdvertisement);

        await _context.SaveChangesAsync();

        return newAdvertisement.Id;
    }

    public async Task<AdvertisementDto[]> GetAllAdvertisementAsync(Guid apiKey)
    {
        AdvertisementDto[] advertising = await _context.Advertisements
            .Include(x=>x.AdsClient)
            .Where(x => x.AdsClient.ApiKey == apiKey && x.AdsClient.IsBlocked)
            .ProjectToType<AdvertisementDto>()
            .ToArrayAsync();

        if (advertising == null)
        {
            throw new InvalidException($"This client does not exist. - {advertising}");
        }

        return advertising;
    }

    public async Task<AdvertisementDto> GetOneAdvertisementAsync(int id, Guid apiKey)
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

        return advertisement.Adapt<AdvertisementDto>();
    }

    public async Task<int> UpdateAdvertisementAsync(UpdateAdvertisementRequest updateAdvertisementRequest, Guid apiKey)
    {
        Advertisement advertisement =
            await _context.Advertisements
                .Where(x=>x.AdsClient.IsBlocked)
                .Include(x=>x.AdsClient)
                .FirstOrDefaultAsync(x => x.Id == updateAdvertisementRequest.Id);

        if (advertisement == null)
        {
            throw new InvalidException($"There is no Advertising with this {updateAdvertisementRequest.Id}");
        }
        
        updateAdvertisementRequest.Adapt(advertisement);

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

        _context.Advertisements.Remove(advertisement);

        await _context.SaveChangesAsync();

        return advertisement.Id;
    }
    
    public Task<bool> ApiKeyIsValid(Guid apiKey)
    {
        return Task.FromResult(_context.Advertisements.Any(x => x.AdsClient.ApiKey == apiKey));
    }
}