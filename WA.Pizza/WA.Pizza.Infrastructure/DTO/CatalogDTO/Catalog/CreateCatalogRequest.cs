using MediatR;

namespace WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog
{
    public record CreateCatalogRequest: IRequest<int>
    {
        public string Name { get; init; }
        public int Quantity { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public int CatalogBrandId { get; init; }
    }
}
