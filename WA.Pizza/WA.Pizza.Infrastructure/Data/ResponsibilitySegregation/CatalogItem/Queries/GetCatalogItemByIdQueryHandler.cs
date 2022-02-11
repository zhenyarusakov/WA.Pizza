using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

namespace WA.Pizza.Infrastructure.Data.Queries;

public class GetCatalogItemByIdQueryHandler: IRequestHandler<GetByIdCatalogItemQuery, CatalogItemsListItem>
{
    private readonly WAPizzaContext _context;

    public GetCatalogItemByIdQueryHandler(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<CatalogItemsListItem> Handle(GetByIdCatalogItemQuery command, CancellationToken cancellationToken)
    {
        CatalogItem catalogItem = await _context
            .CatalogItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (catalogItem == null)
        {
            return null;
        }

        return catalogItem.Adapt<CatalogItemsListItem>();
    }
}