using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface ICatalogItemService
    {
        Task<CatalogItemDto> GetCatalogItemAsync(int id);
        Task<CatalogItemDto[]> GetCatalogItemsAsync();
        Task<CatalogItemDto> CreateCatalogItemAsync(CatalogItemForModifyDto modifyDto);
        Task<CatalogItemDto> UpdateCatalogItemAsync(CatalogItemForModifyDto modifyDto);
        Task DeleteCatalogItemAsync(int id);
    }
}
