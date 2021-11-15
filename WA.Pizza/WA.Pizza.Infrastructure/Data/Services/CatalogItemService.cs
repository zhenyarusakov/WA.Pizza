using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class CatalogItemService: ICatalogItemService
    {
        private readonly WAPizzaContext _context;

        public CatalogItemService(WAPizzaContext context)
        {
            _context = context;
        }

        public async Task<CatalogItem> GetCatalogItemAsync(int id)
        {
            return await _context.CatalogItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CatalogItem[]> GetCatalogItemsAsync()
        {
            return await _context.CatalogItems
                .Include(x => x.CatalogBrand)
                .Include(x => x.CatalogType)
                .ToArrayAsync();
        }

        public async Task<CatalogItem> CreateCatalogItemAsync(CatalogItem catalogItem)
        {
            _context.CatalogItems.Add(catalogItem);

            await _context.SaveChangesAsync();

            return catalogItem;
        }

        public async Task<CatalogItem> UpdateCatalogItemAsync(int id)
        {
            var catalogItemUpdate = await _context.CatalogItems.FirstOrDefaultAsync(x => x.Id == id);

            if (catalogItemUpdate == null)
            {
                throw new ArgumentNullException($"There is no CatalogItem with this {id}");
            }

            _context.Update(catalogItemUpdate);

            await _context.SaveChangesAsync();

            return catalogItemUpdate;
        }

        public async Task DeleteCatalogItemAsync(int id)
        {
            var catalogItemDelete = await _context.CatalogItems.FirstOrDefaultAsync(x => x.Id == id);

            if (catalogItemDelete == null)
            {
                throw new ArgumentNullException($"There is no CatalogItem with this {id}");
            }

            _context.Remove(catalogItemDelete);

            await _context.SaveChangesAsync();
        }
    }
}
