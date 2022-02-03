using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.AdvertisingDTO;

namespace WA.Pizza.Api.Controllers;

public class AdvertisingController: BaseApiController
{
    private readonly IAdvertisingService _advertisingService;

    public AdvertisingController(IAdvertisingService advertisingService)
    {
        _advertisingService = advertisingService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdvertising([FromBody] CreateAdvertisingRequest advertisingRequest)
    {
        int result = await _advertisingService.CreateAdvertisingAsync(advertisingRequest);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAdvertising()
    {
        AdvertisingDto[] result = await _advertisingService.GetAllAdvertisingAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdvertising(int id)
    {
        AdvertisingDto result = await _advertisingService.GetOneAdvertisingAsync(id);

        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAdvertising([FromBody] UpdateAdvertisingRequest updateAdvertisingRequest)
    {
        int result = await _advertisingService.UpdateAdvertisingAsync(updateAdvertisingRequest);

        return Ok(result);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> RemoveAdvertising(int id)
    {
        await _advertisingService.RemoveAdvertisingAsync(id);

        return Ok();
    }
}