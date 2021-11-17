using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface ICatalogService
    {
        Task<CatalogDto> GetCatalogAsync(int id);
        Task<CatalogDto[]> GetCatalogsAsync();
    }
}
