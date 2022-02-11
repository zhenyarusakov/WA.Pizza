using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

namespace WA.Pizza.Infrastructure.Data;

public class DeleteCatalogItemCommandHandler: IRequestHandler<DeleteCatalogItemCommand, int>
{
    private readonly WAPizzaContext _context;

    public DeleteCatalogItemCommandHandler(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteCatalogItemCommand command, CancellationToken cancellationToken)
    {
        CatalogItem catalogItemDelete = await _context.CatalogItems.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (catalogItemDelete == null)
        {
            throw new ArgumentNullException($"There is no CatalogItem with this {command.Id}");
        }

        _context.CatalogItems.Remove(catalogItemDelete);

        await _context.SaveChangesAsync(cancellationToken);

        return command.Id;
    }
}