using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class BasketService: IBasketService
    {
        private readonly WAPizzaContext _context;

        public BasketService(WAPizzaContext context)
        {
            _context = context;
        }

        public async Task<Basket> GetBasketAsync(int id)
        {
            return await _context.Baskets
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Basket[]> GetBasketsAsync()
        {
            return await _context.Baskets
                .Include(x => x.BasketItems)
                .ToArrayAsync();
        }

        public async Task<Basket> CreateBasketAsync(Basket basket)
        {
            _context.Baskets.Add(basket);

            await _context.SaveChangesAsync();

            return basket;
        }

        public async Task<Basket> UpdateBasketAsync(int id)
        {
            var basketUpdate = await _context.Baskets.FirstOrDefaultAsync(x => x.Id == id);

            if (basketUpdate == null)
            {
                throw new ArgumentNullException($"There is no Basket with this {id}");
            }

            _context.Update(basketUpdate);

            await _context.SaveChangesAsync();

            return basketUpdate;
        }

        public async Task DeleteBasketAsync(int id)
        {
            var basketDelete = await _context.Baskets.FirstOrDefaultAsync(x => x.Id == id);

            if (basketDelete == null)
            {
                throw new ArgumentNullException($"There is no Basket with this {id}");
            }

            _context.Remove(basketDelete);

            await _context.SaveChangesAsync();
        }
    }
}
