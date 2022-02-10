using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;
using Catalog = WA.Pizza.Core.Entities.CatalogDomain.CatalogItem;

namespace WA.Pizza.Infrastructure.Data.ResponsibilitySegregation.CatalogItem.Commands;

public class CreateCatalogItemCommand: IRequestHandler<CreateCatalogRequest, int>
{
    private readonly WAPizzaContext _context;

    public CreateCatalogItemCommand(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCatalogRequest request, CancellationToken cancellationToken)
    {
        Catalog catalogItem = request.Adapt<Catalog>();
            
        _context.CatalogItems.Add(catalogItem);

        await _context.SaveChangesAsync();

        return catalogItem.Id;
    }
}