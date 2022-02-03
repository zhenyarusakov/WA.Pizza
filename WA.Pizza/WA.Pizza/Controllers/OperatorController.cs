using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.ClientDto;

namespace WA.Pizza.Api.Controllers;

public class OperatorController: BaseApiController
{
    private readonly IOperatorService _operatorService;

    public OperatorController(IOperatorService operatorService)
    {
        _operatorService = operatorService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewClient(CreateClientRequest clientRequest)
    {
        Guid result = await _operatorService.CreateNewClientAsync(clientRequest);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveClient(int id)
    {
        await _operatorService.RemoveClientAsync(id);

        return Ok();
    }
}