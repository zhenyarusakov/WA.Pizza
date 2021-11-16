using System.Threading.Tasks;
using WA.Pizza.Core.Entities.CatalogDomain;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface ICatalogBrandService
    {
        Task<CatalogBrand> GetCatalogBrandAsync(int id);
        Task<CatalogBrand[]> GetCatalogBrandsAsync();
        Task<CatalogBrand> CreateCatalogBrandAsync(CatalogBrand catalogBrand);
        Task<CatalogBrand> UpdateCatalogBrandAsync(CatalogBrand catalogBrand);
        Task DeleteCatalogBrandAsync(int id);
    }
}
