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
    public async Task<IActionResult> CreateAdvertising([FromBody] CreateAdvertisingRequest advertisingRequest)
    {
        int result = await _advertisingDataService.CreateAdvertisingAsync(advertisingRequest);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAdvertising()
    {
        AdvertisingDto[] result = await _advertisingDataService.GetAllAdvertisingAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdvertising(int id)
    {
        AdvertisingDto result = await _advertisingDataService.GetOneAdvertisingAsync(id);

        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAdvertising([FromBody] UpdateAdvertisingRequest updateAdvertisingRequest)
    {
        int result = await _advertisingDataService.UpdateAdvertisingAsync(updateAdvertisingRequest);

        return Ok(result);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> RemoveAdvertising(int id)
    {
        await _advertisingDataService.RemoveAdvertisingAsync(id);

        return Ok();
    }
}