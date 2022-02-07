using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

    [HttpPost("CreateNewClient")]
    [SwaggerOperation(Summary = "Creates new advertisement")]
    [SwaggerResponse(400, "Malformed adsClientRequest")]
    [SwaggerResponse(201, "New AdsClient created")]
    [ProducesResponseType(typeof(long), 201)]
    public async Task<IActionResult> CreateNewClient(CreateAdsClientRequest adsClientRequest)
    {
        Guid result = await _adsClientDataService.CreateNewAdsClientAsync(adsClientRequest);

        return Ok(result);
    }

    [HttpDelete("RemovedClient")]
    [SwaggerOperation(Summary = "Remove AdsClient")]
    [SwaggerResponse(204, "AdsClient Remove")]
    [SwaggerResponse(404, "AdsClient not found")]
    [ProducesResponseType(typeof(long), 204)]
    public async Task<IActionResult> RemoveClient(int id)
    {
        await _adsClientDataService.RemoveAdsClientAsync(id);

        return Ok();
    }

    [HttpPut("UpdateAdsClient")]
    [SwaggerOperation(Summary = "Updates existing advertisement")]
    [SwaggerResponse(400, "Malformed adsClientRequest")]
    [SwaggerResponse(404, "AdsClient not found")]
    [SwaggerResponse(200, "AdsClient updated")]
    [ProducesResponseType(typeof(long), 200)]
    public async Task<IActionResult> UpdateAdsClient(UpdateAdsClientRequest adsClientRequest)
    {
        var result = await _adsClientDataService.UpdateAdsClientAsync(adsClientRequest);

        return Ok(result);
    }

    [HttpPost("BlockClient")]
    [SwaggerOperation(Summary = "Creates new advertisement")]
    [SwaggerResponse(400, "Malformed id")]
    [SwaggerResponse(201, "Client blocking")]
    [ProducesResponseType(typeof(long), 201)]
    public async Task<IActionResult> BlockClient(int id)
    {
        var result = await _adsClientDataService.BlockClientAsync(id);

        return Ok(result);
    }

    [HttpPost("UnlockClient")]
    [SwaggerOperation(Summary = "Creates new advertisement")]
    [SwaggerResponse(400, "Malformed id")]
    [SwaggerResponse(201, "Client unlock")]
    [ProducesResponseType(typeof(long), 201)]
    public async Task<IActionResult> UnlockClient(int id)
    {
        var result = await _adsClientDataService.UnlockClientAsync(id);

        return Ok(result);
    }
}