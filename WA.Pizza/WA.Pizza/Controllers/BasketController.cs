using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Api.Controllers
{
    public class BasketController: BaseApiController
    {
        private readonly IBasketDataService _service;

        public BasketController(IBasketDataService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetBaskets()
        {
            BasketDto[] result = await _service.GetAllBasketsAsync();

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateBasketRequest modifyDto)
        {
            int result = await _service.CreateBasketAsync(modifyDto);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBasket([FromBody] UpdateBasketItemRequest modifyDto)
        {
            int result = await _service.UpdateBasketItemAsync(modifyDto);

            return Ok(result);
        }
    }
}
