using System;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogItem;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class CatalogItemService: ICatalogItemService
    {
        private readonly WAPizzaContext _context;
        private readonly IMapper _mapper;

        public CatalogItemService(WAPizzaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<CatalogItemDto> GetCatalogItemAsync(int id)
        {
            return _context.CatalogItems
                .AsNoTracking()
                .ProjectToType<CatalogItemDto>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<CatalogItemDto[]> GetCatalogItemsAsync()
        {
            return _context.CatalogItems
                .Include(x => x.CatalogBrand)
                .Include(x => x.CatalogType)
                .ProjectToType<CatalogItemDto>()
                .ToArrayAsync();
        }

        public async Task<CatalogItemDto> CreateCatalogItemAsync(CatalogItemForModifyDto modifyDto)
        {
            var catalogItemDto = _mapper.Map<CatalogItem>(modifyDto);

            _context.CatalogItems.Add(catalogItemDto);

            await _context.SaveChangesAsync();

            return _mapper.Map<CatalogItemDto>(catalogItemDto);
        }

        public async Task<CatalogItemDto> UpdateCatalogItemAsync(CatalogItemForModifyDto modifyDto)
        {
            var catalogItemUpdate = await _context.CatalogItems.FirstOrDefaultAsync(x => x.Id == modifyDto.Id);

            if (catalogItemUpdate == null)
            {
                throw new ArgumentNullException($"There is no CatalogItem with this {modifyDto.Id}");
            }

            _mapper.Map(modifyDto, catalogItemUpdate);

            _context.Update(catalogItemUpdate);

            await _context.SaveChangesAsync();

            return _mapper.Map<CatalogItemDto>(catalogItemUpdate);
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
