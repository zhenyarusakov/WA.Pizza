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
            var basket = await _context.Baskets
                .AsNoTracking()
                .ProjectToType<BasketDto>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (basket == null)
            {
                throw new ArgumentNullException($"There is no Basket with this {id}");
            }

            return basket;
        }

        public async Task<BasketDto[]> GetBasketsAsync()
        {
            return await _context.Baskets
                .Include(x => x.BasketItems)
                .ProjectToType<BasketDto>()
                .ToArrayAsync();
        }

        public async Task<BasketDto> CreateBasketAsync(CreateOrUpdateBasketRequest basketRequest)
        {
            var basketAdd = basketRequest.Adapt<Basket>();

            _context.Baskets.Add(basketAdd);

            await _context.SaveChangesAsync();

            return basketAdd.Adapt<BasketDto>();
        }

        public async Task<BasketDto> UpdateBasketAsync(CreateOrUpdateBasketRequest basketRequest)
        {
            var basket = await _context.Baskets.FirstOrDefaultAsync(x=>x.Id == basketRequest.Id);

            if (basket == null)
            {
                throw new ArgumentNullException($"There is no Basket with this {basketRequest.Id}");
            }

            basketRequest.Adapt(basket);

            _context.Update(basket);

            await _context.SaveChangesAsync();
            
            return basketRequest.Adapt<BasketDto>();
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
