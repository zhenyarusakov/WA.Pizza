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

        public Task<CatalogItem> GetCatalogItemAsync(int id)
        {
            return _context.CatalogItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<CatalogItem[]> GetCatalogItemsAsync()
        {
            return _context.CatalogItems
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

        public async Task<CatalogItem> UpdateCatalogItemAsync(CatalogItem catalogItem)
        {
            var catalogItemUpdate = await _context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogItem.Id);

            if (catalogItemUpdate == null)
            {
                throw new ArgumentNullException($"There is no CatalogItem with this {catalogItem.Id}");
            }

            catalogItemUpdate.CatalogBrand = catalogItem.CatalogBrand;
            catalogItemUpdate.CatalogType = catalogItem.CatalogType;
            catalogItemUpdate.Description = catalogItem.Description;
            catalogItemUpdate.Name = catalogItem.Name;
            catalogItemUpdate.Price = catalogItem.Price;

            _context.Update(catalogItemUpdate);

            await _context.SaveChangesAsync();

            return catalogItemUpdate;
        }

        public async Task DeleteCatalogItemAsync(int id)
        {

            _context.CatalogItems.Remove(new CatalogItem()
            {
                Id = id
            });

            await _context.SaveChangesAsync();
        }
    }
}
