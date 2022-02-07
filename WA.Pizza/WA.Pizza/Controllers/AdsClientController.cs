using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;
using WA.Pizza.Infrastructure.DTO.AdsClientDTO;

namespace WA.Pizza.Api.Controllers;

public class AdsClientController: BaseApiController
{
    private readonly IAdsClientDataService _adsClientDataService;

    public AdsClientController(IAdsClientDataService adsClientDataService)
    {
        _adsClientDataService = adsClientDataService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewClient(CreateAdsClientRequest adsClientRequest)
    {
        Guid result = await _adsClientDataService.CreateNewAdsClientAsync(adsClientRequest);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveClient(int id)
    {
        await _adsClientDataService.RemoveAdsClientAsync(id);

        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAdsClient(UpdateAdsClientRequest adsClientRequest)
    {
        var result = await _adsClientDataService.UpdateAdsClientAsync(adsClientRequest);

        return Ok(result);
    }

    [HttpPost("Block")]
    public async Task<IActionResult> BlockClient(int id)
    {
        var result = await _adsClientDataService.BlockClientAsync(id);

        return Ok(result);
    }

    [HttpPost("Unlock")]
    public async Task<IActionResult> UnlockClient(int id)
    {
        var result = await _adsClientDataService.UnlockClientAsync(id);

        return Ok(result);
    }
}