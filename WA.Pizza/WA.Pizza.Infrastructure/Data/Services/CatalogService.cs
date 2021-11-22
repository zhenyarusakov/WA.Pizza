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

        public Task<CatalogItemDto> GetCatalogAsync(int id)
        {
            var catalog = _context.CatalogItems
                .AsNoTracking()
                .ProjectToType<CatalogItemDto>()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (catalog == null)
            {
                throw new ArgumentNullException($"There is no Catalog with this {id}");
            }

            return catalog;
        }

        public Task<CatalogItemDto[]> GetCatalogsAsync()
        {
            return _context.CatalogItems
                .Include(x => x.CatalogBrand)
                .ProjectToType<CatalogItemDto>()
                .ToArrayAsync();
        }

        public async Task<CatalogItemDto> CreateCatalogItemAsync(CreateCatalogRequest catalogRequest)
        {
            var catalogItemDto = catalogRequest.Adapt<CatalogItem>();

            _context.CatalogItems.Add(catalogItemDto);

            await _context.SaveChangesAsync();

            return catalogItemDto.Adapt<CatalogItemDto>();
        }
        
        public async Task<CatalogItemDto> UpdateCatalogItemAsync(UpdateCatalogRequest catalogRequest)
        {
            var catalogItemUpdate = await _context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogRequest.Id);

            if (catalogItemUpdate == null)
            {
                throw new ArgumentNullException($"There is no CatalogItem with this {catalogRequest.Id}");
            }
            
            catalogRequest.Adapt(catalogItemUpdate);

            _context.Update(catalogItemUpdate);

            await _context.SaveChangesAsync();

            return catalogItemUpdate.Adapt<CatalogItemDto>();
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
