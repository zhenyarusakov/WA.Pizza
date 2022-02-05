using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.AdvertisingDTO;

namespace WA.Pizza.Api.Controllers;

public class AdvertisingController: BaseApiController
{
    private readonly IAdvertisingDataService _advertisingDataService;

    public AdvertisingController(IAdvertisingDataService advertisingDataService)
    {
        _advertisingDataService = advertisingDataService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdvertising(
        [FromBody] CreateAdvertisingRequest advertisingRequest, 
        [Required][FromHeader] Guid apiKey)
    {
        int result = await _advertisingDataService.CreateAdvertisingAsync(advertisingRequest, apiKey);

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAdvertising([Required][FromHeader] Guid apiKey)
    {
        AdvertisingDto[] result = await _advertisingDataService.GetAllAdvertisingAsync(apiKey);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdvertising(int id, [Required][FromHeader] Guid apiKey)
    {
        AdvertisingDto result = await _advertisingDataService.GetOneAdvertisingAsync(id, apiKey);

        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAdvertising(
        [FromBody] UpdateAdvertisingRequest updateAdvertisingRequest, 
        [Required][FromHeader] Guid apiKey)
    {
        int result = await _advertisingDataService.UpdateAdvertisingAsync(updateAdvertisingRequest, apiKey);

        return Ok(result);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> RemoveAdvertising(int id, [Required][FromHeader] Guid apiKey)
    {
        await _advertisingDataService.RemoveAdvertisingAsync(id, apiKey);

        return Ok();
    }
}