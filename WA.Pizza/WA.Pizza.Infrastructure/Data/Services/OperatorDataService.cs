using System;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.ClientDto;

namespace WA.Pizza.Infrastructure.Data.Services;

public class OperatorDataService: IOperatorService
{
    private readonly WAPizzaContext _context;

    public OperatorDataService(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateNewClientAsync(CreateClientRequest clientRequest)
    {
        Client client = clientRequest.Adapt<Client>();
        
        client.ApiToken = Guid.NewGuid();

        _context.Clients.Add(client);

        await _context.SaveChangesAsync();

        return client.ApiToken;
    }

    public async Task RemoveClientAsync(int id)
    {
        var clientId = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);

        if (clientId == null)
        {
            throw new ArgumentNullException($"Current {id} does not exist");
        }

        _context.Clients.Remove(clientId);

        await _context.SaveChangesAsync();
    }
}