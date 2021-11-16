using System;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class BasketService: IBasketService
    {
        private readonly WAPizzaContext _context;
        private readonly IMapper _mapper;

        public BasketService(WAPizzaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var basketDto = _mapper.Map<Basket>(modifyDto);

            _context.Baskets.Add(basketDto);

            await _context.SaveChangesAsync();

            return _mapper.Map<BasketDto>(basketDto);
        }

        public async Task<BasketDto> UpdateBasketAsync(BasketForModifyDto modifyDto)
        {
            var basketUpdate = await _context.Baskets.FirstOrDefaultAsync(x=>x.Id == modifyDto.Id);

            if (basketUpdate == null)
            {
                throw new ArgumentNullException($"There is no Basket with this {modifyDto.Id}");
            }

            _mapper.Map(modifyDto, basketUpdate);

            _context.Update(basketUpdate);

            await _context.SaveChangesAsync();

            return _mapper.Map<BasketDto>(modifyDto);
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
