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

    [HttpGet("{id}", Name = "GetClient")]
    [SwaggerOperation(Summary = "Get One Client")]
    [SwaggerResponse(400, "Malformed AdsClientDto")]
    [SwaggerResponse(200, "GetAllClients")]
    [ProducesResponseType(typeof(AdsClientDto), 200)]
    public async Task<IActionResult> GetClient(int id)
    {
        var result = await _adsClientDataService.GetClientAsync(id);

        return Ok(result);
    }

    [HttpGet("GetAllClients")]
    [SwaggerOperation(Summary = "Get All Clients")]
    [SwaggerResponse(400, "Malformed AdsClientDto")]
    [SwaggerResponse(200, "GetAllClients")]
    [ProducesResponseType(typeof(AdsClientDto[]), 200)]
    public async Task<IActionResult> GetAllClients()
    {
        var result = await _adsClientDataService.GetAllClientsAsync();

        return Ok(result);
    }

    [HttpPost("CreateNewClient")]
    [SwaggerOperation(Summary = "Creates new AdsClient")]
    [SwaggerResponse(400, "Malformed adsClientRequest")]
    [SwaggerResponse(201, "New AdsClient created")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateNewClient(CreateAdsClientRequest createRequest)
    {
        Guid result = await _adsClientDataService.CreateNewAdsClientAsync(createRequest);

        return Ok(result);
    }

    [HttpDelete("RemovedClient")]
    [SwaggerOperation(Summary = "Remove AdsClient")]
    [SwaggerResponse(204, "AdsClient Remove")]
    [SwaggerResponse(404, "AdsClient not found")]
    [ProducesResponseType(typeof(int), 204)]
    public async Task<IActionResult> RemoveClient(int id)
    {
        await _adsClientDataService.RemoveAdsClientAsync(id);

        return Ok();
    }

    [HttpPut("UpdateAdsClient")]
    [SwaggerOperation(Summary = "Updates AdsClient")]
    [SwaggerResponse(400, "Malformed adsClientRequest")]
    [SwaggerResponse(404, "AdsClient not found")]
    [SwaggerResponse(200, "AdsClient updated")]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<IActionResult> UpdateAdsClient(UpdateAdsClientRequest updateRequest)
    {
        var result = await _adsClientDataService.UpdateAdsClientAsync(updateRequest);

        return Ok(result);
    }

    [HttpPost("BlockClient")]
    [SwaggerOperation(Summary = "Client blocking")]
    [SwaggerResponse(400, "Malformed id")]
    [SwaggerResponse(201, "Client blocking")]
    [ProducesResponseType(typeof(int), 201)]
    public async Task<IActionResult> BlockClient(int id)
    {
        var result = await _adsClientDataService.BlockClientAsync(id);

        return Ok(result);
    }

    [HttpPost("UnlockClient")]
    [SwaggerOperation(Summary = "Unlock Client")]
    [SwaggerResponse(400, "Malformed id")]
    [SwaggerResponse(201, "Client unlock")]
    [ProducesResponseType(typeof(int), 201)]
    public async Task<IActionResult> UnlockClient(int id)
    {
        var result = await _adsClientDataService.UnlockClientAsync(id);

        return Ok(result);
    }
}