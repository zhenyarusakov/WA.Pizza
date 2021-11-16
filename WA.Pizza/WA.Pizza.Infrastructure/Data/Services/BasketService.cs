using System;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class BasketService: IBasketService
    {
        private readonly WAPizzaContext _context;
        public BasketService(WAPizzaContext context)
        {
            _context = context;
        }

        public async Task<BasketDto> GetBasketAsync(int id)
        {
            return await _context.Baskets
                .AsNoTracking()
                .ProjectToType<BasketDto>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BasketDto[]> GetBasketsAsync()
        {
            return await _context.Baskets
                .Include(x => x.BasketItems)
                .ProjectToType<BasketDto>()
                .ToArrayAsync();
        }

        public async Task<BasketDto> CreateBasketAsync(BasketForModifyDto modifyDto)
        {
            var basketDto = modifyDto.Adapt<Basket>();

            _context.Baskets.Add(basketDto);

            await _context.SaveChangesAsync();

            return basketDto.Adapt<BasketDto>();
        }

        public async Task<BasketDto> UpdateBasketAsync(BasketForModifyDto modifyDto)
        {
            var basketUpdate = await _context.Baskets.FirstOrDefaultAsync(x=>x.Id == modifyDto.Id);

            if (basketUpdate == null)
            {
                throw new ArgumentNullException($"There is no Basket with this {modifyDto.Id}");
            }
            
            TypeAdapter.Adapt(modifyDto, basketUpdate);

            _context.Update(basketUpdate);

            await _context.SaveChangesAsync();
            
            return TypeAdapter.Adapt<BasketDto>(modifyDto);
        }

        public async Task DeleteBasketAsync(int id)
        {
            _context.Baskets.Remove(new Basket()
            {
                Id = id
            });

            await _context.SaveChangesAsync();
        }
    }
}
