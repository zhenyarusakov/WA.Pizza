using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

namespace WA.Pizza.Infrastructure.Data;

public class UpdateCatalogItemCommandHandler : IRequestHandler<UpdateCatalogItemCommand, int>
{
    private readonly WAPizzaContext _context;

    public UpdateCatalogItemCommandHandler(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateCatalogItemCommand command, CancellationToken cancellationToken)
    {
        CatalogItem catalogItem = await _context.CatalogItems.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (catalogItem == null)
        {
            throw new ArgumentNullException($"There is no CatalogItem with this {command.Id}");
        }
            
        command.Adapt(catalogItem);

        _context.Update(catalogItem);

        await _context.SaveChangesAsync(cancellationToken);

        return catalogItem.Id;
    }
}