using System;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.AdsClientDTO;

namespace WA.Pizza.Infrastructure.Data.Services;

public class OperatorDataDataService: IOperatorDataService
{
    private readonly WAPizzaContext _context;

    public OperatorDataDataService(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateNewAdsClientAsync(CreateAdsClientRequest adsClientRequest)
    {
        AdsClient adsClient = adsClientRequest.Adapt<AdsClient>();
        
        adsClient.ApiKey = Guid.NewGuid();

        _context.AdsClients.Add(adsClient);

        await _context.SaveChangesAsync();

        return adsClient.ApiKey;
    }

    public async Task RemoveAdsClientAsync(int id)
    {
        AdsClient clientId = await _context.AdsClients.FirstOrDefaultAsync(x => x.Id == id);

        if (clientId == null)
        {
            throw new ArgumentNullException($"Current {id} does not exist");
        }

        _context.AdsClients.Remove(clientId);

        await _context.SaveChangesAsync();
    }
}