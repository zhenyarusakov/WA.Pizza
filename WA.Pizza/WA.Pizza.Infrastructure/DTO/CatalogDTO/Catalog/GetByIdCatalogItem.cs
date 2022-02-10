using MediatR;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

public class GetByIdCatalogItem :IRequest<CatalogItemDto>
{
    public int Id { get; init; }
}