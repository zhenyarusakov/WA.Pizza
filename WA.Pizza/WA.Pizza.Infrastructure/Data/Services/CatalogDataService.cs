using System;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class CatalogDataService: ICatalogDataService
    {
        private readonly WAPizzaContext _context;

        public CatalogDataService(WAPizzaContext context)
        {
            _context = context;
        }

        public async Task<CatalogItemDto> GetCatalogAsync(int id)
        {
            CatalogItem catalogItem = await _context.CatalogItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (catalogItem == null)
            {
                throw new ArgumentNullException($"There is no Catalog item with this {id}");
            }

            return catalogItem.Adapt<CatalogItemDto>();
        }

        public Task<CatalogItemDto[]> GetAllCatalogsAsync()
        {
            return _context.CatalogItems
                .Include(x => x.CatalogBrand)
                .ProjectToType<CatalogItemDto>()
                .ToArrayAsync();
        }

        public async Task<int> CreateCatalogItemAsync(CreateCatalogRequest catalogRequest)
        {
            CatalogItem catalogItem = catalogRequest.Adapt<CatalogItem>();

            _context.CatalogItems.Add(catalogItem);

            await _context.SaveChangesAsync();

            return catalogItem.Id;
        }
        
        public async Task<int> UpdateCatalogItemAsync(UpdateCatalogRequest catalogRequest)
        {
            CatalogItem catalogItem = await _context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogRequest.Id);

            if (catalogItem == null)
            {
                throw new ArgumentNullException($"There is no CatalogItem with this {catalogRequest.Id}");
            }
            
            catalogRequest.Adapt(catalogItem);

            _context.Update(catalogItem);

            await _context.SaveChangesAsync();

            return catalogItem.Id;
        }

        public async Task DeleteCatalogItemAsync(int id)
        {
            CatalogItem catalogItemDelete = await _context.CatalogItems
                .FirstOrDefaultAsync(x => x.Id == id);

            if (catalogItemDelete == null)
            {
                throw new ArgumentNullException($"There is no CatalogItem with this {id}");
            }

            _context.CatalogItems.RemoveRange(catalogItemDelete);

            await _context.SaveChangesAsync();
        }

    }
}
