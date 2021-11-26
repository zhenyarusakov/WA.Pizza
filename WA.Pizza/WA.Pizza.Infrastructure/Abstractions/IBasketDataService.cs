using System.Threading.Tasks;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IBasketDataService
    {
        Task<BasketDto[]> GetAllBasketsAsync();
        Task<BasketDto> CreateBasketAsync(CreateBasketRequest basketRequest);
        Task<BasketDto> UpdateBasketAsync(UpdateBasketRequest basketRequest);
        Task CleanBasketItemsAsync(int id);
    }
}
