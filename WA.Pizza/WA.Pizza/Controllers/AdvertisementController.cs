using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;
using WA.Pizza.Infrastructure.DTO.AdvertisementDTO;
using WA.Pizza.Infrastructure.ErrorHandling;

namespace WA.Pizza.Api.Controllers;

public class AdvertisementController: BaseApiController
{
    private readonly IAdvertisementDataService _advertisementDataService;

    public AdvertisementController(IAdvertisementDataService advertisementDataService)
    {
        _advertisementDataService = advertisementDataService;
    }

    [HttpPost("CreateAdvertisement")]
    [SwaggerOperation(Summary = "Creates new advertisement")]
    [SwaggerResponse(201, "New advertisement created")]
    [SwaggerResponse(400, "Malformed createAdvertisementRequest")]
    [SwaggerResponse(401, "Provided API key is not a valid key")]
    [ProducesResponseType(typeof(int), 201)]
    public async Task<IActionResult> CreateAdvertisement(
        [FromBody] CreateAdvertisementRequest createRequest, 
        [Required][FromHeader] Guid apiKey)
    {
        if (!await _advertisementDataService.ApiKeyIsValid(apiKey))
        {
            throw new InvalidException($"invalid ApiKey - {apiKey}");
        }
        
        int result = await _advertisementDataService.CreateAdvertisementAsync(createRequest, apiKey);

        return Ok(result);
    }
    
    [HttpGet("GetAllAdvertisement")]
    [SwaggerOperation(Summary = "Returns all advertisements of client with given API key")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400, "Malformed API key")]
    [SwaggerResponse(401, "Provided API key is not a valid key")]
    [ProducesResponseType(typeof(AdvertisementDto[]), 200)]
    public async Task<IActionResult> GetAllAdvertisement([Required][FromHeader] Guid apiKey)
    {
        if (!await _advertisementDataService.ApiKeyIsValid(apiKey))
        {
            throw new InvalidException($"invalid ApiKey - {apiKey}");
        }
        
        AdvertisementDto[] result = await _advertisementDataService.GetAllAdvertisementAsync(apiKey);

        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetAllAdvertisement")]
    [SwaggerOperation(Summary = "Returns one advertisements of client with given API key")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400, "Malformed API key")]
    [SwaggerResponse(401, "Provided API key is not a valid key")]
    [ProducesResponseType(typeof(AdvertisementDto), 200)]
    public async Task<IActionResult> GetAdvertisement(int id, [Required][FromHeader] Guid apiKey)
    {
        if (!await _advertisementDataService.ApiKeyIsValid(apiKey))
        {
            throw new InvalidException($"invalid ApiKey - {apiKey}");
        }
        
        AdvertisementDto result = await _advertisementDataService.GetOneAdvertisementAsync(id, apiKey);

        return Ok(result);
    }
    
    [HttpPut("UpdateAdvertisement")]
    [SwaggerOperation(Summary = "Updates existing advertisement")]
    [SwaggerResponse(200, "Advertisement updated")]
    [SwaggerResponse(400, "Malformed updateAdvertisementRequest")]
    [SwaggerResponse(401, "Provided API key is not a valid key")]
    [SwaggerResponse(404, "Advertisement not found")]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<IActionResult> UpdateAdvertisement(
        [FromBody] UpdateAdvertisementRequest updateRequest, 
        [Required][FromHeader] Guid apiKey)
    {
        if (!await _advertisementDataService.ApiKeyIsValid(apiKey))
        {
            throw new InvalidException($"invalid ApiKey - {apiKey}");
        }
        
        int result = await _advertisementDataService.UpdateAdvertisementAsync(updateRequest, apiKey);

        return Ok(result);
    }

    [HttpDelete("id", Name = "RemoveAdvertisement")]
    [SwaggerOperation(Summary = "Remove advertisement")]
    [SwaggerResponse(204, "Advertisement Remove")]
    [SwaggerResponse(401, "Provided API key is not a valid key")]
    [SwaggerResponse(404, "Advertisement not found")]
    [ProducesResponseType(typeof(int), 204)]
    public async Task<IActionResult> RemoveAdvertisement(int id, [Required][FromHeader] Guid apiKey)
    {
        if (!await _advertisementDataService.ApiKeyIsValid(apiKey))
        {
            throw new InvalidException($"invalid ApiKey - {apiKey}");
        }
        
        await _advertisementDataService.RemoveAdvertisementAsync(id, apiKey);

        return Ok();
    }
}