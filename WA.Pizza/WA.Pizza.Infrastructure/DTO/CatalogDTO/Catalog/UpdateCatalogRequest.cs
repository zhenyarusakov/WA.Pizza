using MediatR;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog
{
    public record UpdateCatalogRequest : IRequest<int>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Quantity { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public int CatalogBrandId { get; init; }
    }
}
