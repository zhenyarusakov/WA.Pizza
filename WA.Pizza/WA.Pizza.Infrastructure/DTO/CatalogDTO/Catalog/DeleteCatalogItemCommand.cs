using MediatR;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

public class DeleteCatalogItemCommand: IRequest<int>
{
    public int Id { get; set; }
}