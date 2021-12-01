using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

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

        public async Task<int> UpdateBasketAsync(UpdateBasketRequest basketRequest)
        {
            Basket basket = await _context.Baskets.FirstOrDefaultAsync(x=>x.Id == basketRequest.Id);

            if (basket == null)
            {
                throw new ArgumentNullException($"There is no Basket with this {basketRequest.Id}");
            }

            if (basketRequest.BasketItems.Any(x => x.Quantity < 1))
            {
                throw new ArgumentException("The number of products cannot be less than one");
            }

            basketRequest.Adapt(basket);

            _context.Baskets.Update(basket);

            await _context.SaveChangesAsync();
            
            return basketRequest.Id;
        }

        public async Task CleanBasketItemsAsync(int id)
        {
            BasketItem[] basketItems = await _context.BasketItems.Where(x => x.BasketId == id).ToArrayAsync();
            
            if (!basketItems.Any())
            {
                throw new ArgumentNullException($"There is no BasketItem with this {id}");
            }

            _context.BasketItems.RemoveRange(basketItems);

            await _context.SaveChangesAsync();
        }
    }
}
