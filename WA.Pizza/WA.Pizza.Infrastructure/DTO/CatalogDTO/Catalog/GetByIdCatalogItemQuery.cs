using MediatR;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

public class GetByIdCatalogItemQuery :IRequest<CatalogItemsListItem>
{
    public int Id { get; init; }
}