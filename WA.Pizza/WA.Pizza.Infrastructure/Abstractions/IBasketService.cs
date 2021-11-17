using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(int id);
        Task<BasketDto[]> GetBasketsAsync();
        Task<BasketDto> CreateBasketAsync(CreateOrUpdateBasketRequest basketRequest);
        Task<BasketDto> UpdateBasketAsync(CreateOrUpdateBasketRequest basketRequest);
        Task DeleteBasketAsync(int id);
    }
}
