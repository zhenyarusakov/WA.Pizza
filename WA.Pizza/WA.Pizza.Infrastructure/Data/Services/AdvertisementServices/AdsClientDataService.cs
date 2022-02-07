using System;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;
using WA.Pizza.Infrastructure.DTO.AdsClientDTO;
using WA.Pizza.Infrastructure.ErrorHandling;
using Client = WA.Pizza.Core.Entities.AdsClient;

namespace WA.Pizza.Infrastructure.Data.Services.AdvertisementServices;

public class AdsClientDataService: IAdsClientDataService
{
    private readonly WAPizzaContext _context;

    public AdsClientDataService(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateNewAdsClientAsync(CreateAdsClientRequest adsClientRequest)
    {
        Client adsClient = adsClientRequest.Adapt<Client>();
        
        adsClient.ApiKey = Guid.NewGuid();

        _context.AdsClients.Add(adsClient);

        await _context.SaveChangesAsync();

        return adsClient.ApiKey;
    }

    public async Task RemoveAdsClientAsync(int id)
    {
        Client clientId = await _context.AdsClients.FirstOrDefaultAsync(x => x.Id == id);

        if (clientId == null)
        {
            throw new InvalidException($"Current {id} does not exist");
        }

        _context.AdsClients.Remove(clientId);

        await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAdsClientAsync(UpdateAdsClientRequest adsClientRequest)
    {
        Client adsClient = await _context.AdsClients.FirstOrDefaultAsync(x => x.Id == adsClientRequest.Id);

        if (adsClient == null)
        {
            throw new InvalidException($"Current {adsClientRequest.Id} does not exist");
        }

        adsClientRequest.Adapt(adsClient);

        _context.AdsClients.Update(adsClient);
        
        await _context.SaveChangesAsync();

        return adsClient.Id;
    }

    public async Task<int> BlockClientAsync(int id)
    {
        Client client = await _context.AdsClients.FirstOrDefaultAsync(x => x.Id == id);

        if (client == null)
        {
            throw new InvalidException($"Current client - {id} does not exist");
        }

        if (client.IsBlocked)
        {
            client.IsBlocked = false;

            await _context.SaveChangesAsync();
        }

        return client.Id;
    }

    public async Task<int> UnlockClientAsync(int id)
    {
        Client client = await _context.AdsClients.FirstOrDefaultAsync(x => x.Id == id);

        if (client == null)
        {
            throw new InvalidException($"Current client - {id} does not exist");
        }
        
        if (!client.IsBlocked)
        {
            client.IsBlocked = true;

            await _context.SaveChangesAsync();
        }
        
        return client.Id;
    }
}