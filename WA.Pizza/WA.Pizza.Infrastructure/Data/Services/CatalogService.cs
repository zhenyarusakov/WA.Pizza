using System;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class CatalogService: ICatalogService
    {
        private readonly WAPizzaContext _context;

        public CatalogService(WAPizzaContext context)
        {
            _context = context;
        }

        public Task<CatalogDto> GetCatalogAsync(int id)
        {
            var catalog = _context.Catalogs
                .AsNoTracking()
                .ProjectToType<CatalogDto>()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (catalog == null)
            {
                throw new ArgumentNullException($"There is no Catalog with this {id}");
            }

            return catalog;
        }

        public Task<CatalogDto[]> GetCatalogsAsync()
        {
            return _context.Catalogs
                .Include(x => x.CatalogBrand)
                .Include(x => x.CatalogType)
                .ProjectToType<CatalogDto>()
                .ToArrayAsync();
        }
        
    }
}
