using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(int id);
        Task<BasketDto[]> GetBasketsAsync();
        Task<BasketDto> CreateBasketAsync(BasketForModifyDto modifyDto);
        Task<BasketDto> UpdateBasketAsync(BasketForModifyDto modifyDto);
        Task DeleteBasketAsync(int id);
    }
}
