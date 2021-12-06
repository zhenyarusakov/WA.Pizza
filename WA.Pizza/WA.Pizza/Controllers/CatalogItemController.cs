using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;

namespace WA.Pizza.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogItemController: Controller
    {
        private readonly ICatalogDataService _service;

        public CatalogItemController(ICatalogDataService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogItem(int id)
        {
            CatalogItemDto result = await _service.GetCatalogAsync(id);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalogItems()
        {
            CatalogItemDto[] result = await _service.GetAllCatalogsAsync();

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatalogItem([FromBody] CreateCatalogRequest modifyDto)
        {
            int result = await _service.CreateCatalogItemAsync(modifyDto);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCatalogItem([FromBody] UpdateCatalogRequest modifyDto)
        {
            int result = await _service.UpdateCatalogItemAsync(modifyDto);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogItem(int id)
        {
            await _service.DeleteCatalogItemAsync(id);

            return Ok();
        }
    }
}
