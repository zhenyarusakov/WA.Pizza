using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

namespace WA.Pizza.Api.Controllers
{
    public class CatalogItemController: BaseApiController
    {
        private readonly IMediator _mediator;

        public CatalogItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogItem(int id)
        {
            var result = await _mediator.Send(new GetByIdCatalogItem{Id = id});

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalogItems()
        {
            var result = await _mediator.Send(new CatalogItemDto());
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatalogItem([FromBody] CreateCatalogRequest catalogRequest)
        {
            int result = await _mediator.Send(catalogRequest);
            
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCatalogItem([FromBody] UpdateCatalogRequest catalogRequest)
        {
            int result = await _mediator.Send(catalogRequest);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogItem(int id)
        {
            int result = await _mediator.Send(new DeleteCatalogItemId{Id = id});
        
            return Ok(result);
        }
    }
}
