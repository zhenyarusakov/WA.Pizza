using System.Threading.Tasks;
using WA.Pizza.Core.Entities.BasketDomain;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IBasketItemService
    {
        Task<BasketItem> GetBasketItemAsync(int id);
        Task<BasketItem[]> GetBasketItemsAsync();
        Task<BasketItem> CreateBasketItemAsync(BasketItem basketItem);
        Task<BasketItem> UpdateBasketItemAsync(int id);
        Task DeleteBasketItemAsync(int id);
    }
}
