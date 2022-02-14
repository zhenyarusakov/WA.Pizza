using MediatR;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

public class DeleteCatalogItemCommand: IRequest<int>
{
    public int Id { get; init; }
}