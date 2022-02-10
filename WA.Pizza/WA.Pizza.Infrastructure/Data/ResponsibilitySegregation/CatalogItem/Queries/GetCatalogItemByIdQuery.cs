using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;
using Catalog = WA.Pizza.Core.Entities.CatalogDomain.CatalogItem;

namespace WA.Pizza.Infrastructure.Data.ResponsibilitySegregation.CatalogItem.Queries;

public class GetCatalogItemByIdQuery: IRequestHandler<GetByIdCatalogItem, CatalogItemDto>
{
    private readonly WAPizzaContext _context;

    public GetCatalogItemByIdQuery(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<CatalogItemDto> Handle(GetByIdCatalogItem request, CancellationToken cancellationToken)
    {
        Catalog catalogItem = await _context
            .CatalogItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (catalogItem == null)
        {
            return null;
        }

        return catalogItem.Adapt<CatalogItemDto>();
    }
}