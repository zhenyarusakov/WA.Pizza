using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IBasketService
    {
        Task<BasketDto[]> GetBasketsAsync();
        Task<BasketDto> UpdateBasketAsync(UpdateBasketRequest basketRequest);
    }
}
