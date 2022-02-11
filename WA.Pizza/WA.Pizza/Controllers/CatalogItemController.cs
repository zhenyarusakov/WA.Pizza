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
            var result = await _mediator.Send(new GetByIdCatalogItemQuery{Id = id});

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalogItems()
        {
            var result = await _mediator.Send(new CatalogItemsListItemQuery());
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatalogItem([FromBody] CreateCatalogItemCommand catalogItemCommand)
        {
            int result = await _mediator.Send(catalogItemCommand);
            
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCatalogItem([FromBody] UpdateCatalogItemCommand catalogItemCommand)
        {
            int result = await _mediator.Send(catalogItemCommand);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogItem(int id)
        {
            int result = await _mediator.Send(new DeleteCatalogItemCommand{Id = id});
        
            return Ok(result);
        }
    }
}
