using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.AdsClientDTO;

namespace WA.Pizza.Api.Controllers;

public class OperatorController: BaseApiController
{
    private readonly IOperatorDataService _operatorDataService;

    public OperatorController(IOperatorDataService operatorDataService)
    {
        _operatorDataService = operatorDataService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewClient(CreateAdsClientRequest adsClientRequest)
    {
        Guid result = await _operatorDataService.CreateNewAdsClientAsync(adsClientRequest);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveClient(int id)
    {
        await _operatorDataService.RemoveAdsClientAsync(id);

        return Ok();
    }
}