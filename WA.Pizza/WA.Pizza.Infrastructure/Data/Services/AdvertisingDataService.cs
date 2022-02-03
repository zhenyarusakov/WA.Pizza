using System;
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

    public async Task<int> CreateAdvertisingAsync(CreateAdvertisingRequest createAdvertisingRequest)
    {
        Advertising newAdvertising = createAdvertisingRequest.Adapt<Advertising>();

        _context.Advertisings.Add(newAdvertising);

        await _context.SaveChangesAsync();

        return newAdvertising.Id;
    }

    public Task<AdvertisingDto[]> GetAllAdvertisingAsync()
    {
        return _context.Advertisings
            .ProjectToType<AdvertisingDto>()
            .ToArrayAsync();
    }

    public async Task<AdvertisingDto> GetOneAdvertisingAsync(int id)
    {
        Advertising advertising = await _context.Advertisings
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (advertising == null)
        {
            throw new ArgumentNullException($"This ad does not exist {id}");
        }

        return advertising.Adapt<AdvertisingDto>();
    }

    public async Task<int> UpdateAdvertisingAsync(UpdateAdvertisingRequest updateAdvertisingRequest)
    {
        Advertising advertising =
            await _context.Advertisings.FirstOrDefaultAsync(x => x.Id == updateAdvertisingRequest.Id);

        if (advertising == null)
        {
            throw new ArgumentNullException($"There is no Advertising with this {updateAdvertisingRequest.Id}");
        }

        updateAdvertisingRequest.Adapt(advertising);

        _context.Update(advertising);

        await _context.SaveChangesAsync();

        return advertising.Id;
    }

    public async Task RemoveAdvertisingAsync(int id)
    {
        Advertising advertising =
            await _context.Advertisings.FirstOrDefaultAsync(x => x.Id == id);

        if (advertising == null)
        {
            throw new ArgumentNullException($"There is no Advertising with this {id}");
        }

        _context.Advertisings.Remove(advertising);

        await _context.SaveChangesAsync();
    }
}