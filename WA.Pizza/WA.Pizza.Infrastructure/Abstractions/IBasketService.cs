using System.Threading.Tasks;
using WA.Pizza.Core.Entities.BasketDomain;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IBasketService
    {
        Task<Basket> GetBasketAsync(int id);
        Task<Basket[]> GetBasketsAsync();
        Task<Basket> CreateBasketAsync(Basket basket);
        Task<Basket> UpdateBasketAsync(int id);
        Task DeleteBasketAsync(int id);
    }
}
