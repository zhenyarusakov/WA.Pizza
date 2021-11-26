using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface ICatalogDataService
    {
        Task<CatalogItemDto> GetCatalogAsync(int id);
        Task<CatalogItemDto[]> GetAllCatalogsAsync();
        Task<CatalogItemDto> CreateCatalogItemAsync(CreateCatalogRequest catalogRequest);
        Task<CatalogItemDto> UpdateCatalogItemAsync(UpdateCatalogRequest catalogRequest);
        Task DeleteCatalogItemAsync(int id);
    }
}

