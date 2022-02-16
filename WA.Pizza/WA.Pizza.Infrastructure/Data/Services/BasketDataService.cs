using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WA.Pizza.Core.Entities;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.CatalogDomain;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class BasketDataService: IBasketDataService
    {
        private readonly WAPizzaContext _context;
        private readonly ILogger<BasketDataService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserInfoProvider _userInfoProvider;

        public BasketDataService(WAPizzaContext context, ILogger<BasketDataService> logger, UserManager<ApplicationUser> userManager, IUserInfoProvider userInfoProvider)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _userInfoProvider = userInfoProvider;
        }

        public Task<BasketDto[]> GetAllBasketsAsync()
        {
            _logger.LogInformation("very good");
            
            return _context.Baskets
                .Include(x => x.BasketItems)
                .ProjectToType<BasketDto>()
                .ToArrayAsync();
        }

        public async Task<int> CreateBasketItemAsync(CreateBasketItemRequest basketItemRequest)
        {
            CatalogItem? catalogItem = await _context.CatalogItems
                .AsNoTracking()
                .FirstOrDefaultAsync(ci => ci.Id == basketItemRequest.CatalogItemId);
            
            if (catalogItem == null)
            {
                _logger.LogError("CatalogItem cannot be {CatalogItemId}", catalogItem?.Id);
                throw new ArgumentNullException($"CatalogItem cannot be  {catalogItem?.Id}");
            }

            BasketItem? basketItem = await _context.BasketItems
                .FirstOrDefaultAsync(bi => 
                    bi.BasketId == basketItemRequest.BasketId && 
                    bi.CatalogItemId == basketItemRequest.CatalogItemId);
        
            var basket = await _context.Baskets
                .Include(x => x.BasketItems)
                .FirstOrDefaultAsync(b => b.Id == basketItemRequest.BasketId);

            if (basketItem != null)
            {
                basketItem.Quantity += basketItemRequest.Quantity;
                basket!.LastModified = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return basketItem.Id;
            }

            basketItem = basketItemRequest.Adapt<BasketItem>();

            if (basket == null)
            {
                string? userName = _userInfoProvider.GetUserName();
                var user = await _userManager.FindByNameAsync(userName ?? "");
                basket = new Basket
                {
                    UserId = user?.Id
                };

                _context.Baskets.Add(basket);
            }

            basket.BasketItems.Add(basketItem);
            basket.LastModified = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return basketItem.Id;
        }
        
        public async Task<int> UpdateBasketItemAsync(UpdateBasketItemRequest updateBasketItemRequest)
        {
            BasketItem? item = await _context.BasketItems.Include(x => x.Basket)
                .FirstOrDefaultAsync(x => x.Id == updateBasketItemRequest.Id);

            if (item == null)
            {
                _logger.LogError($"BasketItem cannot be {updateBasketItemRequest.Id}");
                throw new ArgumentNullException($"BasketItem cannot be  {updateBasketItemRequest.Id}");
            }

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
                _logger.LogError($"There is no BasketItem item with this {basketId}");
                throw new ArgumentNullException($"There is no BasketItem with this {basketId}");
            }

            _context.BasketItems.RemoveRange(basketItems);

            await _context.SaveChangesAsync();
        }
    }
}
