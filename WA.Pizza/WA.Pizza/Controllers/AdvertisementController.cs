using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.Abstractions.AdvertisementInterface;
using WA.Pizza.Infrastructure.DTO.AdvertisingDTO;

namespace WA.Pizza.Api.Controllers;

public class AdvertisementController: BaseApiController
{
    private readonly IAdvertisementDataService _advertisementDataService;

    public AdvertisementController(IAdvertisementDataService advertisementDataService)
    {
        _advertisementDataService = advertisementDataService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdvertisement(
        [FromBody] CreateAdvertisingRequest advertisingRequest, 
        [Required][FromHeader] Guid apiKey)
    {
        int result = await _advertisementDataService.CreateAdvertisementAsync(advertisingRequest, apiKey);

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAdvertisement([Required][FromHeader] Guid apiKey)
    {
        AdvertisingDto[] result = await _advertisementDataService.GetAllAdvertisementAsync(apiKey);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdvertisement(int id, [Required][FromHeader] Guid apiKey)
    {
        AdvertisingDto result = await _advertisementDataService.GetOneAdvertisementAsync(id, apiKey);

        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAdvertisement(
        [FromBody] UpdateAdvertisingRequest updateAdvertisingRequest, 
        [Required][FromHeader] Guid apiKey)
    {
        int result = await _advertisementDataService.UpdateAdvertisementAsync(updateAdvertisingRequest, apiKey);

        return Ok(result);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> RemoveAdvertisement(int id, [Required][FromHeader] Guid apiKey)
    {
        await _advertisementDataService.RemoveAdvertisementAsync(id, apiKey);

        return Ok();
    }
}