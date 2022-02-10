using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;
using Catalog = WA.Pizza.Core.Entities.CatalogDomain.CatalogItem;

namespace WA.Pizza.Infrastructure.Data.ResponsibilitySegregation.CatalogItem.Commands;

public class UpdateCatalogItemCommand : IRequestHandler<UpdateCatalogRequest, int>
{
    private readonly WAPizzaContext _context;

    public UpdateCatalogItemCommand(WAPizzaContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateCatalogRequest request, CancellationToken cancellationToken)
    {
        Catalog catalogItem = await _context.CatalogItems.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (catalogItem == null)
        {
            throw new ArgumentNullException($"There is no CatalogItem with this {request.Id}");
        }
            
        request.Adapt(catalogItem);

        _context.Update(catalogItem);

        await _context.SaveChangesAsync();

        return catalogItem.Id;
    }
}