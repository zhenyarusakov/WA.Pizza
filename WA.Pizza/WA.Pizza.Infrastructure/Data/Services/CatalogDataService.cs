using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.BasketDomain;
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

        public async Task<CatalogItemDto> CreateCatalogItemAsync(CreateCatalogRequest catalogRequest)
        {
            CatalogItem catalogItemDto = catalogRequest.Adapt<CatalogItem>();

            _context.CatalogItems.Add(catalogItemDto);

            await _context.SaveChangesAsync();

            return catalogItemDto.Adapt<CatalogItemDto>();
        }
        
        public async Task<CatalogItemDto> UpdateCatalogItemAsync(UpdateCatalogRequest catalogRequest)
        {
            CatalogItem catalogItemUpdate = await _context.CatalogItems.FirstOrDefaultAsync(x => x.Id == catalogRequest.Id);

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
