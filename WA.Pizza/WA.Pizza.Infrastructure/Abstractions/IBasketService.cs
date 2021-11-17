using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(int id);
        Task<BasketDto[]> GetBasketsAsync();
        Task<BasketDto> CreateBasketAsync(BasketForModifyDto basketModify);
        Task<BasketDto> UpdateBasketAsync(BasketForModifyDto basketModify);
        Task DeleteBasketAsync(int id);
    }
}
