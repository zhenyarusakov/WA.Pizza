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

        public async Task<CatalogBrand> GetCatalogBrandAsync(int id)
        {
            return await _context.CatalogBrands
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CatalogBrand[]> GetCatalogBrandsAsync()
        {
            return await _context.CatalogBrands
                .Include(x => x.CatalogItems)
                .ToArrayAsync();
        }

        public async Task<CatalogBrand> CreateCatalogBrandAsync(CatalogBrand catalogBrand)
        {
            _context.CatalogBrands.Add(catalogBrand);

            await _context.SaveChangesAsync();

            return catalogBrand;
        }

        public async Task<CatalogBrand> UpdateCatalogBrandAsync(int id)
        {
            var catalogBrandUpdate = await _context.CatalogBrands.FirstOrDefaultAsync(x => x.Id == id);

            if(catalogBrandUpdate == null)
            {
                throw new ArgumentNullException($"There is no CatalogBrand with this {id}");
            }

            _context.Update(catalogBrandUpdate);

            await _context.SaveChangesAsync();

            return catalogBrandUpdate;
        }

        public async Task DeleteCatalogBrandAsync(int id)
        {
            var catalogBrandDelete = await _context.CatalogBrands.FirstOrDefaultAsync(x => x.Id == id);

            if (catalogBrandDelete == null)
            {
                throw new ArgumentNullException($"There is no CatalogBrand with this {id}");
            }

            _context.Remove(catalogBrandDelete);

            await _context.SaveChangesAsync();
        }
    }
}
