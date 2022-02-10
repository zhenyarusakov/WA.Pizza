using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.Data.ResponsibilitySegregation.CatalogItem.Queries;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

namespace WA.Pizza.Api.Controllers
{
    public class CatalogItemController: BaseApiController
    {
        private readonly ICatalogDataService _service;

        public CatalogItemController(ICatalogDataService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogItem(int id)
        {
            var result = await _service.GetCatalogAsync(id);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalogItems()
        {
            var result = await _service.GetAllCatalogsAsync();
            
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
            int result = await _service.DeleteCatalogItemAsync(id);
        
            return Ok(result);
        }
    }
}
