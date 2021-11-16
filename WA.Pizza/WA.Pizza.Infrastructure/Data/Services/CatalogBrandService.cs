using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class CatalogBrandService: ICatalogBrandService
    {
        private readonly WAPizzaContext _context;

        public CatalogBrandService(WAPizzaContext context)
        {
            _context = context;
        }

        public Task<CatalogBrand> GetCatalogBrandAsync(int id)
        {
            return _context.CatalogBrands
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<CatalogBrand[]> GetCatalogBrandsAsync()
        {
            return _context.CatalogBrands
                .Include(x => x.CatalogItems)
                .ToArrayAsync();
        }

        public async Task<CatalogBrand> CreateCatalogBrandAsync(CatalogBrand catalogBrand)
        {
            _context.CatalogBrands.Add(catalogBrand);

            await _context.SaveChangesAsync();

            return catalogBrand;
        }

        public async Task<CatalogBrand> UpdateCatalogBrandAsync(CatalogBrand catalogBrand)
        {
            var catalogBrandUpdate = await _context.CatalogBrands.FirstOrDefaultAsync(x => x.Id == catalogBrand.Id);

            if(catalogBrandUpdate == null)
            {
                throw new ArgumentNullException($"There is no CatalogBrand with this {catalogBrand.Id}");
            }

            catalogBrandUpdate.CatalogItems = catalogBrand.CatalogItems;
            catalogBrandUpdate.Description = catalogBrand.Description;
            catalogBrandUpdate.Name = catalogBrand.Name;

            _context.Update(catalogBrandUpdate);

            await _context.SaveChangesAsync();

            return catalogBrandUpdate;
        }

        public async Task DeleteCatalogBrandAsync(int id)
        {
            _context.CatalogBrands.Remove(new CatalogBrand()
            {
                Id = id
            });

            await _context.SaveChangesAsync();
        }
    }
}
