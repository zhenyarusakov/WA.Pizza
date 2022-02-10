using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface ICatalogDataService
    {
        Task<CatalogItemDto> GetCatalogAsync(int id);
        Task<CatalogItemDto[]> GetAllCatalogsAsync();
        Task<int> CreateCatalogItemAsync(CreateCatalogRequest catalogRequest);
        Task<int> UpdateCatalogItemAsync(UpdateCatalogRequest catalogRequest);
        Task<int> DeleteCatalogItemAsync(int id);
    }
}

