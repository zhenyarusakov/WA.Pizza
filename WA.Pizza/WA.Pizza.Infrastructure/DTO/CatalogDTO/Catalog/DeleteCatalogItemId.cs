using MediatR;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

public class DeleteCatalogItemId: IRequest<int>
{
    public int Id { get; set; }
}