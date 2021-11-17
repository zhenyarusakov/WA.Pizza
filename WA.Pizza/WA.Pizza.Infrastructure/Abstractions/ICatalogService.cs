using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface ICatalogService
    {
        Task<CatalogItemDto> GetCatalogAsync(int id);
        Task<CatalogItemDto[]> GetCatalogsAsync();
        Task<CatalogItemDto> CreateCatalogItemAsync(CreateOrUpdateCatalogRequest catalogRequest);
        Task<CatalogItemDto> UpdateCatalogItemAsync(CreateOrUpdateCatalogRequest catalogRequest);
        Task DeleteCatalogItemAsync(int id);
    }
}

