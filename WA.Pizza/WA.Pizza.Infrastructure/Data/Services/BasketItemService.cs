using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class BasketItemService: IBasketItemService
    {
        private readonly WAPizzaContext _context;

        public BasketItemService(WAPizzaContext context)
        {
            _context = context;
        }

        public async Task<BasketItem> GetBasketItemAsync(int id)
        {
            return await _context.BasketItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BasketItem[]> GetBasketItemsAsync()
        {
            return await _context.BasketItems
                .ToArrayAsync();
        }

        public async Task<BasketItem> CreateBasketItemAsync(BasketItem basketItem)
        {
            _context.BasketItems.Add(basketItem);

            await _context.SaveChangesAsync();

            return basketItem;
        }

        public async Task<BasketItem> UpdateBasketItemAsync(int id)
        {
            var basketItemUpdate = await _context.BasketItems.FirstOrDefaultAsync(x => x.Id == id);

            if (basketItemUpdate == null)
            {
                throw new ArgumentNullException($"There is no BasketItem with this {id}");
            }

            _context.Update(basketItemUpdate);

            await _context.SaveChangesAsync();

            return basketItemUpdate;
        }

        public async Task DeleteBasketItemAsync(int id)
        {
            var basketItemDelete = await _context.BasketItems.FirstOrDefaultAsync(x => x.Id == id);

            if (basketItemDelete == null)
            {
                throw new ArgumentNullException($"There is no BasketItem with this {id}");
            }

            _context.Remove(basketItemDelete);

            await _context.SaveChangesAsync();
        }
    }
}
