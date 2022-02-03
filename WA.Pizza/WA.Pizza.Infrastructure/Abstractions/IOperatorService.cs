using System;
using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.ClientDto;

namespace WA.Pizza.Infrastructure.Abstractions;

public interface IOperatorService
{
    Task<Guid> CreateNewClientAsync(CreateClientRequest clientRequest);
    Task RemoveClientAsync(int id);
    // Task<int> BlockClientAsync(int id);
}