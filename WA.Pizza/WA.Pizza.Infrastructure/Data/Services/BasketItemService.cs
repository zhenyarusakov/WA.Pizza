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

        public Task<BasketItem> GetBasketItemAsync(int id)
        {
            return _context.BasketItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<BasketItem[]> GetBasketItemsAsync()
        {
            return _context.BasketItems.ToArrayAsync();
        }

        public async Task<BasketItem> CreateBasketItemAsync(BasketItem basketItem)
        {
            _context.BasketItems.Add(basketItem);

            await _context.SaveChangesAsync();

            return basketItem;
        }

        public async Task<BasketItem> UpdateBasketItemAsync(BasketItem basketItem)
        {
            var basketItemUpdate = await _context.BasketItems.FirstOrDefaultAsync(x => x.Id == basketItem.Id);

            if (basketItemUpdate == null)
            {
                throw new ArgumentNullException($"There is no BasketItem with this {basketItem.Id}");
            }

            basketItemUpdate.Name = basketItemUpdate.Name;
            basketItemUpdate.Basket = basketItemUpdate.Basket;
            basketItemUpdate.Description = basketItemUpdate.Description;
            basketItemUpdate.Price = basketItemUpdate.Price;

            _context.Update(basketItemUpdate);

            await _context.SaveChangesAsync();

            return basketItemUpdate;
        }

        public async Task DeleteBasketItemAsync(int id)
        {
            _context.BasketItems.Remove(new BasketItem()
            {
                Id = id
            });

            await _context.SaveChangesAsync();
        }
    }
}
