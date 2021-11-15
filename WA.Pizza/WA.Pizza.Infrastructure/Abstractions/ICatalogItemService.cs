using System.Threading.Tasks;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface ICatalogItemService
    {
        Task<CatalogItem> GetCatalogItemAsync(int id);
        Task<CatalogItem[]> GetCatalogItemsAsync();
        Task<CatalogItem> CreateCatalogItemAsync(CatalogItem catalogItem);
        Task<CatalogItem> UpdateCatalogItemAsync(int id);
        Task DeleteCatalogItemAsync(int id);
    }
}
