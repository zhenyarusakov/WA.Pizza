using System.Threading.Tasks;
using MediatR;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class CatalogDataService: ICatalogDataService
    {
        private readonly IMediator _mediator;

        public CatalogDataService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<CatalogItemDto> GetCatalogAsync(int id)
        {
            return _mediator.Send(new GetByIdCatalogItem{Id = id});
        }

        public Task<CatalogItemDto[]> GetAllCatalogsAsync()
        {
            return _mediator.Send(new CatalogItemDto());
        }

        public async Task<int> CreateCatalogItemAsync(CreateCatalogRequest catalogRequest)
        {
            return await _mediator.Send(catalogRequest);
        }
        
        public async Task<int> UpdateCatalogItemAsync(UpdateCatalogRequest catalogRequest)
        {
            return await _mediator.Send(catalogRequest);
        }

        public async Task<int> DeleteCatalogItemAsync(int id)
        {
            return await _mediator.Send(new DeleteCatalogItemId{Id = id});
        }

    }
}
