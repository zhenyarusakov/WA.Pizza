using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

namespace WA.Pizza.Infrastructure.Data.Queries;
public class GetAllCatalogItemQueryHandler: IRequestHandler<CatalogItemsListItemQuery, CatalogItemsListItem[]>
{
    private readonly WAPizzaContext _context;

    public GetAllCatalogItemQueryHandler(WAPizzaContext context)
    {
        _context = context;
    }

    public Task<CatalogItemsListItem[]> Handle(CatalogItemsListItemQuery query, CancellationToken cancellationToken)
    {
        return _context.CatalogItems
            .Include(x => x.CatalogBrand)
            .AsNoTracking()
            .ProjectToType<CatalogItemsListItem>()
            .ToArrayAsync(cancellationToken);
    }
}