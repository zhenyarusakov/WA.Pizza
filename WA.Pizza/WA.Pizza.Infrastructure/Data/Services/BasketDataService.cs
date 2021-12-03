using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class BasketDataService: IBasketDataService
    {
        private readonly WAPizzaContext _context;
        public BasketDataService(WAPizzaContext context)
        {
            _context = context;
        }

        public Task<BasketDto[]> GetAllBasketsAsync()
        {
            return _context.Baskets
                .Include(x => x.BasketItems)
                .ProjectToType<BasketDto>()
                .ToArrayAsync();
        }

        public async Task<int> CreateBasketAsync(CreateBasketRequest basketRequest)
        {
            Basket basket = basketRequest.Adapt<Basket>();

            _context.Baskets.Add(basket);

            await _context.SaveChangesAsync();
            
            return basket.Id;
        }

        public async Task<int> UpdateBasketItemAsync(UpdateBasketItemRequest updateBasketItemRequest)
        {
            BasketItem item = await _context.BasketItems.Include(x => x.Basket)
                .FirstOrDefaultAsync(x => x.Id == updateBasketItemRequest.Id);

            if (item == null)
                throw new ArgumentNullException($"BasketItem {updateBasketItemRequest.Id}");

            if (updateBasketItemRequest.Quantity <= 0)
                _context.BasketItems.Remove(item);
            else
                item.Quantity = updateBasketItemRequest.Quantity;

            item.Basket!.LastModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return item.Id;
        }

        public async Task CleanBasketAsync(int basketId)
        {
            BasketItem[] basketItems = await _context.BasketItems.Where(x => x.BasketId == basketId).ToArrayAsync();
            
            if (!basketItems.Any())
            {
                throw new ArgumentNullException($"There is no BasketItem with this {basketId}");
            }

            _context.BasketItems.RemoveRange(basketItems);

            await _context.SaveChangesAsync();
        }
    }
}
