using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.Data;

public class CreateCatalogItemCommandHandler: IRequestHandler<CreateCatalogItemCommand, int>
{
    private readonly WAPizzaContext _context;

    public CreateCatalogItemCommandHandler(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCatalogItemCommand command, CancellationToken cancellationToken)
    {
        CatalogItem catalogItem = command.Adapt<CatalogItem>();
            
        _context.CatalogItems.Add(catalogItem);

        await _context.SaveChangesAsync(cancellationToken);

        return catalogItem.Id;
    }
}