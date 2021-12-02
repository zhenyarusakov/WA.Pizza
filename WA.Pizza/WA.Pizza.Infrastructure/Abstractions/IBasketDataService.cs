using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IBasketDataService
    {
        Task<BasketDto[]> GetAllBasketsAsync();
        Task<int> CreateBasketAsync(CreateBasketRequest basketRequest);
        Task<int> UpdateBasketItemAsync(UpdateBasketItemRequest basketRequest);
        Task CleanBasketAsync(int id);
    }
}
