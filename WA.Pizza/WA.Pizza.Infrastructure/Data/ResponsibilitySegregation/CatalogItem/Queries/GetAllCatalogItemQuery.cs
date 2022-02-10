using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

namespace WA.Pizza.Infrastructure.Data.ResponsibilitySegregation.CatalogItem.Queries;
public class GetAllCatalogItemQuery: IRequestHandler<CatalogItemDto, CatalogItemDto[]>
{
    private readonly WAPizzaContext _context;

    public GetAllCatalogItemQuery(WAPizzaContext context)
    {
        _context = context;
    }

    public Task<CatalogItemDto[]> Handle(CatalogItemDto request, CancellationToken cancellationToken)
    {
        return _context.CatalogItems
            .Include(x => x.CatalogBrand)
            .AsNoTracking()
            .ProjectToType<CatalogItemDto>()
            .ToArrayAsync();
    }
}