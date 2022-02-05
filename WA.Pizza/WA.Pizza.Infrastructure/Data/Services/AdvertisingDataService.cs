using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.AdvertisingDTO;

namespace WA.Pizza.Infrastructure.Data.Services;

public class AdvertisingDataService: IAdvertisingDataService
{
    private readonly WAPizzaContext _context;

    public AdvertisingDataService(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAdvertisingAsync(CreateAdvertisingRequest createAdvertisingRequest, Guid apiKey)
    {
        Advertising newAdvertising = createAdvertisingRequest.Adapt<Advertising>();

        var client = await _context.AdsClients.FirstOrDefaultAsync(x => x.Id == createAdvertisingRequest.AdsClientId);

        if (client == null)
        {
            throw new InvalidOperationException("kek");
        }
        
        if (client.ApiKey != apiKey)
        {
            throw new InvalidOperationException($"invalid ApiKey - {apiKey}");
        }
        
        _context.Advertisings.Add(newAdvertising);

        await _context.SaveChangesAsync();

        return newAdvertising.Id;
    }

    public async Task<AdvertisingDto[]> GetAllAdvertisingAsync(Guid apiKey)
    {
        AdvertisingDto[] advertising = await _context.Advertisings
            .Include(x=>x.AdsClient)
            .ProjectToType<AdvertisingDto>()
            .ToArrayAsync();

        var key = advertising.Where(x => x.ClientDto.ApiToken == apiKey);
        
        if (key == null)
        {
            throw new InvalidOperationException($"invalid ApiKey - {apiKey}");
        }

        return advertising;
    }

    public async Task<AdvertisingDto> GetOneAdvertisingAsync(int id, Guid apiKey)
    {
        Advertising advertising = await _context.Advertisings
            .AsNoTracking()
            .Include(x => x.AdsClient)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (advertising!.AdsClient.ApiKey != apiKey)
        {
            throw new InvalidOperationException($"invalid ApiKey - {apiKey}");
        }
        
        if (advertising == null)
        {
            throw new ArgumentNullException($"This ad does not exist {id}");
        }

        return advertising.Adapt<AdvertisingDto>();
    }

    public async Task<int> UpdateAdvertisingAsync(UpdateAdvertisingRequest updateAdvertisingRequest, Guid apiKey)
    {
        Advertising advertising =
            await _context.Advertisings
                .Include(x=>x.AdsClient)
                .FirstOrDefaultAsync(x => x.Id == updateAdvertisingRequest.Id);

        if (advertising!.AdsClient.ApiKey != apiKey)
        {
            throw new InvalidOperationException($"invalid ApiKey - {apiKey}");
        }
        
        if (advertising == null)
        {
            throw new ArgumentNullException($"There is no Advertising with this {updateAdvertisingRequest.Id}");
        }

        updateAdvertisingRequest.Adapt(advertising);

        _context.Update(advertising);

        await _context.SaveChangesAsync();

        return advertising.Id;
    }

    public async Task<int> RemoveAdvertisingAsync(int id, Guid apiKey)
    {
        Advertising advertising =
            await _context.Advertisings
                .Include(x=>x.AdsClient)
                .FirstOrDefaultAsync(x => x.Id == id);

        if (advertising!.AdsClient.ApiKey != apiKey)
        {
            throw new InvalidOperationException($"invalid ApiKey - {apiKey}");
        }
        
        if (advertising == null)
        {
            throw new ArgumentNullException($"There is no Advertising with this {id}");
        }

        _context.Advertisings.Remove(advertising);

        await _context.SaveChangesAsync();

        return advertising.Id;
    }
}