using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class BasketDataService: IBasketService
    {
        private readonly WAPizzaContext _context;
        public BasketDataService(WAPizzaContext context)
        {
            _context = context;
        }

        public Task<BasketDto[]> GetBasketsAsync()
        {
            return _context.Baskets
                .Include(x => x.BasketItems)
                .ProjectToType<BasketDto>()
                .ToArrayAsync();
        }

        public async Task<BasketDto> UpdateBasketAsync(UpdateBasketRequest basketRequest)
        {
            var basket = await _context.Baskets.FirstOrDefaultAsync(x=>x.Id == basketRequest.Id);

            if (basket == null)
            {
                throw new ArgumentNullException($"There is no Basket with this {basketRequest.Id}");
            }

            if (basketRequest.BasketItems.Any(x => x.Quantity < 1))
            {
                throw new ArgumentException("The number of products cannot be less than one");
            }

            basketRequest.Adapt(basket);

            _context.Update(basket);

            await _context.SaveChangesAsync();
            
            return basketRequest.Adapt<BasketDto>();
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
