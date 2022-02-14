using MediatR;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog
{
    public record CatalogItemsListItem : IRequest<CatalogItemsListItem[]>
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public int Quantity { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public int CatalogBrandId { get; init; }
    }
}
