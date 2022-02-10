using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;
using Catalog = WA.Pizza.Core.Entities.CatalogDomain.CatalogItem;

namespace WA.Pizza.Infrastructure.Data.ResponsibilitySegregation.CatalogItem.Commands;

public class DeleteCatalogItemCommand: IRequestHandler<DeleteCatalogItemId, int>
{
    private readonly WAPizzaContext _context;

    public DeleteCatalogItemCommand(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteCatalogItemId request, CancellationToken cancellationToken)
    {
        Catalog catalogItemDelete = await _context.CatalogItems
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (catalogItemDelete == null)
        {
            throw new ArgumentNullException($"There is no CatalogItem with this {request.Id}");
        }

        _context.CatalogItems.RemoveRange(catalogItemDelete);

        await _context.SaveChangesAsync();

        return request.Id;
    }
}