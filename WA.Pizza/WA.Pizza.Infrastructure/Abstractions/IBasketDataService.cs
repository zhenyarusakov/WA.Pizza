using System.Threading.Tasks;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IBasketDataService
    {
        Task<BasketDto[]> GetAllBasketsAsync();
        Task<int> CreateBasketAsync(CreateBasketRequest basketRequest);
        Task<int> UpdateBasketAsync(UpdateBasketRequest basketRequest);
        Task CleanBasketItemsAsync(int id);
    }
}
